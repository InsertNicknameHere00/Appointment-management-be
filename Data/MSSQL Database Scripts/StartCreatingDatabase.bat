sqlcmd /S localhost /d master -i"1_Create_haircutSalonDB_Database.sql"
sqlcmd /S localhost /d haircutSalonDB -i"2_Create_Roles_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"3_Create_Users_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"4_Create_SalonServices_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"5_Create_Appointments_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"6_Create_AdminServices_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"7_Create_Review_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"8_Create_Product_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"9_Create_Orders_Table.sql"
sqlcmd /S localhost /d haircutSalonDB -i"10_Create_OrderItem_Table.sql"

pause