BEGIN TRANSACTION

CREATE TABLE [dbo].[EventStore](
    [Id] [uniqueidentifier] NOT NULL,
    [CreatedAt] [datetime2] NOT NULL,
    [Sequence] [int] IDENTITY(1,1) NOT NULL,
    [Version] [int] NOT NULL,
    [Name] [nvarchar](250) NOT NULL,
    [AggregateId] [nvarchar](250) NOT NULL,
    [Data] [nvarchar](max) NOT NULL,
    [Aggregate] [nvarchar](250) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [ConcurrencyCheckIndex] ON [dbo].[EventStore]
([Version] ASC, [AggregateId] ASC) WITH (
    PAD_INDEX = OFF, 
    STATISTICS_NORECOMPUTE = OFF, 
    SORT_IN_TEMPDB = OFF, 
    IGNORE_DUP_KEY = OFF, 
    DROP_EXISTING = OFF, ONLINE = OFF, 
    ALLOW_ROW_LOCKS = ON, 
    ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


COMMIT