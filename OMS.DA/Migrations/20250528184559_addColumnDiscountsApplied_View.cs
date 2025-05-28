using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class addColumnDiscountsApplied_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW DiscountsApplied AS
                                   SELECT D.DiscountId,
                                   	      S.ServiceId,
                                   	      S.Name AS ServiceName,
                                   	      S.Price AS ServicePrice,
                                   	      CASE D .ClientType 
                                   	      WHEN 0 THEN 'عادي' 
                                   	      WHEN 1 THEN 'محامي' 
                                   	      WHEN 2 THEN 'غير ذلك'
                                   	      ELSE 'غير معرف' END AS ClientType, 
                                   	      CAST(D.DiscountPercentage AS varchar(10)) + '%' AS Discount
                                   FROM   dbo.Discounts AS D INNER JOIN dbo.Services AS S
	                                      ON D.ServiceId = S.ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW DiscountsApplied AS
                                   SELECT D.DiscountId,
                                   	      S.Name AS ServiceName,
                                   	      S.Price AS ServicePrice,
                                   	      CASE D .ClientType 
                                   	      WHEN 0 THEN 'عادي' 
                                   	      WHEN 1 THEN 'محامي' 
                                   	      WHEN 2 THEN 'غير ذلك'
                                   	      ELSE 'غير معرف' END AS ClientType, 
                                   	      CAST(D.DiscountPercentage AS varchar(10)) + '%' AS Discount
                                   FROM   dbo.Discounts AS D INNER JOIN dbo.Services AS S
	                                      ON D.ServiceId = S.ServiceId");
        }
    }
}
