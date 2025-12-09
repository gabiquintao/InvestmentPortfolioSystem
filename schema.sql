-- =============================================
-- Investment Portfolio Management System
-- Database Schema for SQL Server
-- Author: [Your Name]
-- Date: December 2025
-- Description: Complete database schema for portfolio management system
-- =============================================

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InvestmentPortfolioDB')
BEGIN
    CREATE DATABASE InvestmentPortfolioDB;
END
GO

USE InvestmentPortfolioDB;
GO

-- =============================================
-- Table: Users
-- Description: Stores user account information
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        FirstName NVARCHAR(50) NULL,
        LastName NVARCHAR(50) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT CK_Users_Email CHECK (Email LIKE '%_@__%.__%')
    );

    CREATE INDEX IX_Users_Email ON Users(Email);
    CREATE INDEX IX_Users_Username ON Users(Username);
END
GO

-- =============================================
-- Table: Portfolios
-- Description: Stores portfolio information for users
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Portfolios')
BEGIN
    CREATE TABLE Portfolios (
        PortfolioId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500) NULL,
        BaseCurrency NVARCHAR(3) NOT NULL DEFAULT 'USD',
        IsDefault BIT NOT NULL DEFAULT 0,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Portfolios_Users FOREIGN KEY (UserId) 
            REFERENCES Users(UserId) ON DELETE CASCADE,
        CONSTRAINT CK_Portfolios_Currency CHECK (LEN(BaseCurrency) = 3)
    );

    CREATE INDEX IX_Portfolios_UserId ON Portfolios(UserId);
    CREATE INDEX IX_Portfolios_Name ON Portfolios(Name);
END
GO

-- =============================================
-- Table: Assets
-- Description: Stores information about tradable assets
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Assets')
BEGIN
    CREATE TABLE Assets (
        AssetId INT IDENTITY(1,1) PRIMARY KEY,
        Symbol NVARCHAR(20) NOT NULL UNIQUE,
        Name NVARCHAR(200) NOT NULL,
        AssetType NVARCHAR(20) NOT NULL,
        Exchange NVARCHAR(50) NULL,
        Currency NVARCHAR(3) NOT NULL DEFAULT 'USD',
        Description NVARCHAR(1000) NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT CK_Assets_Type CHECK (AssetType IN ('Stock', 'Crypto', 'ETF', 'Bond', 'Commodity', 'Index'))
    );

    CREATE INDEX IX_Assets_Symbol ON Assets(Symbol);
    CREATE INDEX IX_Assets_Type ON Assets(AssetType);
    CREATE INDEX IX_Assets_Exchange ON Assets(Exchange);
END
GO

-- =============================================
-- Table: PortfolioPositions
-- Description: Current holdings in each portfolio
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PortfolioPositions')
BEGIN
    CREATE TABLE PortfolioPositions (
        PositionId INT IDENTITY(1,1) PRIMARY KEY,
        PortfolioId INT NOT NULL,
        AssetId INT NOT NULL,
        Quantity DECIMAL(18, 8) NOT NULL,
        AveragePurchasePrice DECIMAL(18, 4) NOT NULL,
        CurrentPrice DECIMAL(18, 4) NULL,
        LastPriceUpdate DATETIME2 NULL,
        TotalInvested AS (Quantity * AveragePurchasePrice) PERSISTED,
        CurrentValue AS (Quantity * ISNULL(CurrentPrice, AveragePurchasePrice)) PERSISTED,
        UnrealizedGainLoss AS (Quantity * (ISNULL(CurrentPrice, AveragePurchasePrice) - AveragePurchasePrice)) PERSISTED,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_PortfolioPositions_Portfolios FOREIGN KEY (PortfolioId) 
            REFERENCES Portfolios(PortfolioId) ON DELETE CASCADE,
        CONSTRAINT FK_PortfolioPositions_Assets FOREIGN KEY (AssetId) 
            REFERENCES Assets(AssetId) ON DELETE CASCADE,
        CONSTRAINT CK_PortfolioPositions_Quantity CHECK (Quantity >= 0),
        CONSTRAINT UQ_Portfolio_Asset UNIQUE (PortfolioId, AssetId)
    );

    CREATE INDEX IX_PortfolioPositions_PortfolioId ON PortfolioPositions(PortfolioId);
    CREATE INDEX IX_PortfolioPositions_AssetId ON PortfolioPositions(AssetId);
END
GO

-- =============================================
-- Table: Transactions
-- Description: Records all buy/sell transactions
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Transactions')
BEGIN
    CREATE TABLE Transactions (
        TransactionId INT IDENTITY(1,1) PRIMARY KEY,
        PortfolioId INT NOT NULL,
        AssetId INT NOT NULL,
        TransactionType NVARCHAR(10) NOT NULL,
        Quantity DECIMAL(18, 8) NOT NULL,
        PricePerUnit DECIMAL(18, 4) NOT NULL,
        TotalAmount AS (Quantity * PricePerUnit) PERSISTED,
        Commission DECIMAL(18, 4) NOT NULL DEFAULT 0,
        TransactionDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Notes NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Transactions_Portfolios FOREIGN KEY (PortfolioId) 
            REFERENCES Portfolios(PortfolioId) ON DELETE CASCADE,
        CONSTRAINT FK_Transactions_Assets FOREIGN KEY (AssetId) 
            REFERENCES Assets(AssetId),
        CONSTRAINT CK_Transactions_Type CHECK (TransactionType IN ('Buy', 'Sell')),
        CONSTRAINT CK_Transactions_Quantity CHECK (Quantity > 0),
        CONSTRAINT CK_Transactions_Price CHECK (PricePerUnit > 0)
    );

    CREATE INDEX IX_Transactions_PortfolioId ON Transactions(PortfolioId);
    CREATE INDEX IX_Transactions_AssetId ON Transactions(AssetId);
    CREATE INDEX IX_Transactions_Date ON Transactions(TransactionDate DESC);
    CREATE INDEX IX_Transactions_Type ON Transactions(TransactionType);
END
GO

-- =============================================
-- Table: PriceAlerts
-- Description: User-defined price alerts for assets
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PriceAlerts')
BEGIN
    CREATE TABLE PriceAlerts (
        AlertId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        AssetId INT NOT NULL,
        TargetPrice DECIMAL(18, 4) NOT NULL,
        AlertType NVARCHAR(10) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        IsTriggered BIT NOT NULL DEFAULT 0,
        TriggeredAt DATETIME2 NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_PriceAlerts_Users FOREIGN KEY (UserId) 
            REFERENCES Users(UserId) ON DELETE CASCADE,
        CONSTRAINT FK_PriceAlerts_Assets FOREIGN KEY (AssetId) 
            REFERENCES Assets(AssetId) ON DELETE CASCADE,
        CONSTRAINT CK_PriceAlerts_Type CHECK (AlertType IN ('Above', 'Below')),
        CONSTRAINT CK_PriceAlerts_Price CHECK (TargetPrice > 0)
    );

    CREATE INDEX IX_PriceAlerts_UserId ON PriceAlerts(UserId);
    CREATE INDEX IX_PriceAlerts_AssetId ON PriceAlerts(AssetId);
    CREATE INDEX IX_PriceAlerts_Active ON PriceAlerts(IsActive) WHERE IsActive = 1;
END
GO

-- =============================================
-- Table: Watchlists
-- Description: User-created watchlists
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Watchlists')
BEGIN
    CREATE TABLE Watchlists (
        WatchlistId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500) NULL,
        IsDefault BIT NOT NULL DEFAULT 0,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Watchlists_Users FOREIGN KEY (UserId) 
            REFERENCES Users(UserId) ON DELETE CASCADE
    );

    CREATE INDEX IX_Watchlists_UserId ON Watchlists(UserId);
END
GO

-- =============================================
-- Table: WatchlistItems
-- Description: Assets in each watchlist
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WatchlistItems')
BEGIN
    CREATE TABLE WatchlistItems (
        WatchlistItemId INT IDENTITY(1,1) PRIMARY KEY,
        WatchlistId INT NOT NULL,
        AssetId INT NOT NULL,
        AddedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Notes NVARCHAR(500) NULL,
        CONSTRAINT FK_WatchlistItems_Watchlists FOREIGN KEY (WatchlistId) 
            REFERENCES Watchlists(WatchlistId) ON DELETE CASCADE,
        CONSTRAINT FK_WatchlistItems_Assets FOREIGN KEY (AssetId) 
            REFERENCES Assets(AssetId) ON DELETE CASCADE,
        CONSTRAINT UQ_Watchlist_Asset UNIQUE (WatchlistId, AssetId)
    );

    CREATE INDEX IX_WatchlistItems_WatchlistId ON WatchlistItems(WatchlistId);
    CREATE INDEX IX_WatchlistItems_AssetId ON WatchlistItems(AssetId);
END
GO

-- =============================================
-- Table: MarketDataCache
-- Description: Caches market data to reduce API calls
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MarketDataCache')
BEGIN
    CREATE TABLE MarketDataCache (
        CacheId INT IDENTITY(1,1) PRIMARY KEY,
        AssetId INT NOT NULL,
        Price DECIMAL(18, 4) NOT NULL,
        OpenPrice DECIMAL(18, 4) NULL,
        HighPrice DECIMAL(18, 4) NULL,
        LowPrice DECIMAL(18, 4) NULL,
        Volume BIGINT NULL,
        Change DECIMAL(18, 4) NULL,
        ChangePercent DECIMAL(10, 4) NULL,
        LastUpdated DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        DataSource NVARCHAR(50) NOT NULL,
        CONSTRAINT FK_MarketDataCache_Assets FOREIGN KEY (AssetId) 
            REFERENCES Assets(AssetId) ON DELETE CASCADE
    );

    CREATE UNIQUE INDEX IX_MarketDataCache_AssetId ON MarketDataCache(AssetId);
    CREATE INDEX IX_MarketDataCache_Updated ON MarketDataCache(LastUpdated DESC);
END
GO

-- =============================================
-- Table: AuditLog
-- Description: Tracks all system changes for security
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AuditLog')
BEGIN
    CREATE TABLE AuditLog (
        AuditId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NULL,
        ActionType NVARCHAR(50) NOT NULL,
        EntityType NVARCHAR(50) NOT NULL,
        EntityId INT NULL,
        OldValue NVARCHAR(MAX) NULL,
        NewValue NVARCHAR(MAX) NULL,
        IpAddress NVARCHAR(50) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_AuditLog_Users FOREIGN KEY (UserId) 
            REFERENCES Users(UserId)
    );

    CREATE INDEX IX_AuditLog_UserId ON AuditLog(UserId);
    CREATE INDEX IX_AuditLog_CreatedAt ON AuditLog(CreatedAt DESC);
    CREATE INDEX IX_AuditLog_EntityType ON AuditLog(EntityType);
END
GO

-- =============================================
-- Stored Procedures
-- =============================================

-- Get Portfolio Summary with Performance Metrics
CREATE OR ALTER PROCEDURE sp_GetPortfolioSummary
    @PortfolioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.PortfolioId,
        p.Name AS PortfolioName,
        p.BaseCurrency,
        COUNT(pp.PositionId) AS TotalPositions,
        SUM(pp.TotalInvested) AS TotalInvested,
        SUM(pp.CurrentValue) AS CurrentValue,
        SUM(pp.UnrealizedGainLoss) AS UnrealizedGainLoss,
        CASE 
            WHEN SUM(pp.TotalInvested) > 0 
            THEN (SUM(pp.UnrealizedGainLoss) / SUM(pp.TotalInvested)) * 100 
            ELSE 0 
        END AS ReturnPercentage
    FROM Portfolios p
    LEFT JOIN PortfolioPositions pp ON p.PortfolioId = pp.PortfolioId
    WHERE p.PortfolioId = @PortfolioId
    GROUP BY p.PortfolioId, p.Name, p.BaseCurrency;
END
GO

-- Get User Portfolio Performance Over Time
CREATE OR ALTER PROCEDURE sp_GetPortfolioHistory
    @PortfolioId INT,
    @StartDate DATETIME2,
    @EndDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.TransactionDate,
        t.TransactionType,
        a.Symbol,
        a.Name AS AssetName,
        t.Quantity,
        t.PricePerUnit,
        t.TotalAmount,
        t.Commission
    FROM Transactions t
    INNER JOIN Assets a ON t.AssetId = a.AssetId
    WHERE t.PortfolioId = @PortfolioId
        AND t.TransactionDate BETWEEN @StartDate AND @EndDate
    ORDER BY t.TransactionDate DESC;
END
GO

-- Check and Trigger Price Alerts
CREATE OR ALTER PROCEDURE sp_CheckPriceAlerts
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE pa
    SET 
        pa.IsTriggered = 1,
        pa.TriggeredAt = GETUTCDATE(),
        pa.IsActive = 0
    FROM PriceAlerts pa
    INNER JOIN MarketDataCache mdc ON pa.AssetId = mdc.AssetId
    WHERE pa.IsActive = 1
        AND (
            (pa.AlertType = 'Above' AND mdc.Price >= pa.TargetPrice) OR
            (pa.AlertType = 'Below' AND mdc.Price <= pa.TargetPrice)
        );

    -- Return triggered alerts
    SELECT 
        pa.AlertId,
        u.Email,
        u.Username,
        a.Symbol,
        a.Name AS AssetName,
        pa.TargetPrice,
        pa.AlertType,
        mdc.Price AS CurrentPrice,
        pa.TriggeredAt
    FROM PriceAlerts pa
    INNER JOIN Users u ON pa.UserId = u.UserId
    INNER JOIN Assets a ON pa.AssetId = a.AssetId
    INNER JOIN MarketDataCache mdc ON pa.AssetId = mdc.AssetId
    WHERE pa.IsTriggered = 1
        AND pa.TriggeredAt >= DATEADD(MINUTE, -5, GETUTCDATE());
END
GO

-- =============================================
-- Sample Data for Testing
-- =============================================

-- Insert sample user (password: Test@123 - hashed)
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'testuser')
BEGIN
    INSERT INTO Users (Username, Email, PasswordHash, FirstName, LastName)
    VALUES ('testuser', 'test@example.com', 
            'AQAAAAIAAYagAAAAEJ3z7QOqL8xQNZ1xK5RvqZ6v1KvZ8YqL9xQNZ1xK5RvqZ6v1KvZ8Yq=', 
            'Test', 'User');
END
GO

-- Insert sample assets
IF NOT EXISTS (SELECT * FROM Assets WHERE Symbol = 'AAPL')
BEGIN
    INSERT INTO Assets (Symbol, Name, AssetType, Exchange, Currency)
    VALUES 
        ('AAPL', 'Apple Inc.', 'Stock', 'NASDAQ', 'USD'),
        ('MSFT', 'Microsoft Corporation', 'Stock', 'NASDAQ', 'USD'),
        ('GOOGL', 'Alphabet Inc.', 'Stock', 'NASDAQ', 'USD'),
        ('BTC-USD', 'Bitcoin', 'Crypto', 'Crypto', 'USD'),
        ('ETH-USD', 'Ethereum', 'Crypto', 'Crypto', 'USD'),
        ('SPY', 'SPDR S&P 500 ETF Trust', 'ETF', 'NYSE', 'USD');
END
GO

PRINT 'Database schema created successfully!';
PRINT 'Tables: Users, Portfolios, Assets, PortfolioPositions, Transactions, PriceAlerts, Watchlists, WatchlistItems, MarketDataCache, AuditLog';
PRINT 'Stored Procedures: sp_GetPortfolioSummary, sp_GetPortfolioHistory, sp_CheckPriceAlerts';
GO