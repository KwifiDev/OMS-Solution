using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToSalesSummary_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW SalesSummary AS
                                   SELECT  D.SaleId, 
                                    	   CL.ClientId,
                                    	   S.Name AS ServiceName,
                                    	   ISNULL(D.Description, 'لا يوجد وصف') AS Description,
                                   	       ISNULL(D.Notes, 'لا يوجد ملاحظات') AS Notes,
                                    	   TotalSales = (CAST(D.Total AS varchar(15)) + ' L.S'), 
                                    	   CASE D.Status
                                    	   WHEN 0 THEN 'غير مكتملة'
                                    	   WHEN 1 THEN 'مكتملة'
                                    	   WHEN 2 THEN 'ملغات'
                                    	   ELSE 'غير معرف' 
                                    	   END AS Status
                                    FROM Sales D
                                    JOIN Clients CL ON D.ClientId = CL.ClientId
                                    JOIN People C ON CL.PersonId = C.PersonId
                                    JOIN Services S ON D.ServiceId = S.ServiceId;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW SalesSummary AS
                                   SELECT  D.SaleId, 
                                    	   CL.ClientId,
                                    	   S.Name AS ServiceName,
                                    	   ISNULL(D.Description, 'لا يوجد وصف') AS Description,
                                    	   TotalSales = (CAST(D.Total AS varchar(15)) + ' L.S'), 
                                    	   CASE D.Status
                                    	   WHEN 0 THEN 'غير مكتملة'
                                    	   WHEN 1 THEN 'مكتملة'
                                    	   WHEN 2 THEN 'ملغات'
                                    	   ELSE 'غير معرف' 
                                    	   END AS Status
                                    FROM Sales D
                                    JOIN Clients CL ON D.ClientId = CL.ClientId
                                    JOIN People C ON CL.PersonId = C.PersonId
                                    JOIN Services S ON D.ServiceId = S.ServiceId;");
        }
    }
}
