SET IDENTITY_INSERT dbo.Providers ON;

INSERT INTO dbo.Providers
    (Id, Name, IsActive, CreatedAt, EndpointUrl, EndpointTimeout, ScheduleCron, ScheduleMaxRetryCount)
VALUES
    (1, N'T-Mobile',  1, GETUTCDATE(), N'https://api.t-mobile.com/v2/products',        15, N'*/30 * * * * *', 3),
    (2, N'Vodafone',  1, GETUTCDATE(), N'https://api.vodafone.com/global/v1/sim-products', 20, N'*/45 * * * * *', 3),
    (3, N'Orange',    1, GETUTCDATE(), N'https://api.orange.com/openapi/v2/products/sim',  10, N'0 */1 * * * *', 3),
    (4, N'AT&T',      1, GETUTCDATE(), N'https://api.att.com/mobility/v3/products',    12, N'*/20 * * * * *', 3),
    (5, N'Verizon',   1, GETUTCDATE(), N'https://api.verizon.com/mvno/v1/catalog',     18, N'0 */2 * * * *', 3),
    (6, N'MTN Group', 1, GETUTCDATE(), N'https://api.mtn.com/africa/v1/sim-catalog',   25, N'*/30 * * * * *', 3);

SET IDENTITY_INSERT dbo.Providers OFF;
GO