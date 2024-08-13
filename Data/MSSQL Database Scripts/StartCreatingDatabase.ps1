Set-ExecutionPolicy "Unrestricted" -Force -Confirm:$false
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope LocalMachine -Force -Confirm:$false
Add-Type -AssemblyName PresentationFramework
	
$MessageBoxTitle = “Create Haircut Salon Database”
$ButtonType = 'YesNo'
$MessageIcon = [System.Windows.MessageBoxImage]::Question

$CreateDatabaseFontMsgBoxBody = “Желаете ли да създатете базата данни?”
$CreateDatabaseFontMsgBox=[System.Windows.MessageBox]::Show($CreateDatabaseFontMsgBoxBody,$MessageboxTitle,$ButtonType,$Messageicon)

switch  ($CreateDatabaseFontMsgBox) {

  'Yes' {

	$files = Get-ChildItem -Filter *.sql
	foreach ($file in $files) {
    sqlcmd /S localhost /d master -i $file.FullName
	}
	pause
  }

  'No' {

	continue
  }
}