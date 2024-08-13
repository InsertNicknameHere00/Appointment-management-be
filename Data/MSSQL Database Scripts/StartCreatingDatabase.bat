for %%G in (*.sql) do sqlcmd /S localhost /d master  -i"%%G"
pause
