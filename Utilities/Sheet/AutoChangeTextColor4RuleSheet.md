
// Đoạn mã này là của App Script trong Google Sheet. 
// Khi thực hiện AppScript, ấn run. Sau đó quay lại Sheet, ở tab sẽ có thêm Number color
// Ấn vào Number Color thì sẽ auto đổi màu text theo quy luật 4 con số. 

```
function onOpen() {
 var ui = SpreadsheetApp.getUi();
 ui.createMenu('Number color')
     .addItem('Thực hiện', 'highlightCells')
     .addToUi();
}
function highlightCells() {
 var sheet = SpreadsheetApp.getActiveSpreadsheet().getActiveSheet();
 var range = sheet.getDataRange();
 var formulas = range.getFormulas();
 var values = range.getValues();
  for (var i = 0; i < values.length; i++) {
   for (var j = 0; j < values[i].length; j++) {
     var cell = sheet.getRange(i + 1, j + 1);
     var value = values[i][j];
     var formula = formulas[i][j];
     if (formula) {
       if (formula.includes('!')) {
         cell.setFontColor("magenta");
       } else if (/^=\s*[A-Za-z]+\d+\s*$/.test(formula)) {
         cell.setFontColor("#34a853");
       } else {
         cell.setFontColor("black");
       }
     } else if (typeof value === 'number') {
       cell.setFontColor("CornflowerBlue");
     }
   }
 }
}
```