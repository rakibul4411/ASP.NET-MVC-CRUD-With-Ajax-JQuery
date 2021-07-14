CREATE PROC Customer.spTransferCustomerData
AS
BEGIN
SET NOCOUNT ON;    
  
DECLARE @id int, @customer_id bigint ,@custome_name varchar(50),@checkId int;

DECLARE customer_cursor CURSOR FOR     
SELECT Id,CustomerId,CustomerName FROM Customer.CustomerWiseBonusRate;

OPEN customer_cursor    
FETCH NEXT FROM customer_cursor    
INTO @id,@customer_id,@custome_name
   
WHILE @@FETCH_STATUS = 0    
BEGIN    
	SELECT @checkId = COUNT(Id) FROM Customer.CustomerInfo WHERE Id = @id
	--print @checkId
	IF @checkId > 0
		BEGIN
			UPDATE Customer.CustomerInfo SET CustomerId = @customer_id, CustomerName = @custome_name WHERE Id = @id
		END
	ELSE
		BEGIN
			INSERT INTO Customer.CustomerInfo(iD,CustomerId,CustomerName) VALUES(@id,@customer_id,@custome_name)
		END
    FETCH NEXT FROM customer_cursor     
	INTO @id,@customer_id,@custome_name 
END     
CLOSE customer_cursor;    
DEALLOCATE customer_cursor; 

SELECT 'Data Transfer Completed' AS 'Message'
-- EXEC Customer.spTransferCustomerData
END