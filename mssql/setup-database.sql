CREATE DATABASE RestaurantDB;
GO

USE RestaurantDB;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Reservations] 
(
    [Id]                    NVARCHAR(255)   NOT NULL PRIMARY KEY,
    [Raw_request]           NVARCHAR(MAX)   NOT NULL,
    [DT]                    DATETIME2       NOT NULL,
    [Validation_result]     INT             NOT NULL,
);
GO

CREATE OR ALTER PROCEDURE [dbo].[prcInsertSuccessReservation]
(
    @Id                 NVARCHAR(255),
    @Raw_request        NVARCHAR(MAX),
    @DT                 DATETIME2,
    @Validation_result  INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Reservations (Id, Raw_request, DT, Validation_result)
    VALUES (@Id, @Raw_request, @DT, @Validation_result);
END
GO

CREATE TABLE [dbo].[SuccesReservationsInserted] 
(
    [Id]                    NVARCHAR(255)   NOT NULL PRIMARY KEY,
    [ReservationId]         NVARCHAR(255)   NOT NULL
);
GO

CREATE OR ALTER PROCEDURE [dbo].[prcInsertSuccessReservationInserted]
(
    @Id                 NVARCHAR(255),
    @ReservationId      NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.SuccesReservationsInserted (Id, ReservationId)
    VALUES (@Id, @ReservationId);
END
GO