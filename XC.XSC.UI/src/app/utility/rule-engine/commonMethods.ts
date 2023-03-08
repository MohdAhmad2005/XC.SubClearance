import { QueryList } from '@angular/core';
import { DxPieChartComponent, DxChartComponent } from 'devextreme-angular';
//import { isNullOrUndefined } from 'util';
import { FilterConfig } from 'src/app/models/rule_engine/gridSettings';

export class CommonMethods {
  
  static makeGridFilter(filterData: FilterConfig[] = [], obj: any) {
    if (Array.isArray(obj.filter[0]) === false && obj.filter[0].length > 1)
      filterData.push({ ColumnName: obj.filter[0], Operator: obj.filter[1], Value: obj.filter.filterValue, LogicalOperator: "" });
    else if (Array.isArray(obj.filter[0]) === false && Array.isArray(obj.filter[1]) === true && obj.filter[0].length === 1 && Array.isArray(obj.filter[1][0]) === false)
      filterData.push({ ColumnName: obj.filter[1][0], Operator: obj.filter[0] + obj.filter[1][1], Value: obj.filter[1].filterValue, LogicalOperator: "" });
    else {
      var prevnotEqual = "";
      obj.filter.forEach((function (val, key) {
        var notEqual = this;
        notEqual = val[0];
        if (typeof (val) === "string" && filterData.length > 0 && val !== '!')
          filterData[filterData.length - 1].LogicalOperator = val;
        else if (Array.isArray(val[key]) === false && val.filterValue !== undefined && (typeof (val[0][0]) === "string" && val[0][0].toLowerCase().indexOf("date") === -1 && val[0][2] !== null))
          filterData.push({ ColumnName: val[0], Operator: val[1], Value: val.filterValue, LogicalOperator: "" });
        else if (Array.isArray(val) === true && val.filterValue && (typeof (val[0][0]) === "string" && val[0][0].toLowerCase().indexOf("date") === -1)) {
          filterData.push({ ColumnName: val[0], Operator: val[1], Value: val.filterValue, LogicalOperator: "" });
        }
        else if (Array.isArray(val) === true && typeof (val[0]) === "string" && val[0].toLocaleLowerCase().indexOf("date") >= 0) {
          filterData.push({ ColumnName: val[0], Operator: val[1], Value: val[2], LogicalOperator: "" });
        }
        else if (Array.isArray(val) === true && typeof (val[0]) === "string" && val[1] === "=" && (val[2] === null || val[2] === "")) {
          filterData.push({ ColumnName: val[0], Operator: val[1], Value: (val[2] === null ? 'null' : ''), LogicalOperator: "" });
        }
        else if (Array.isArray(val)) {
          //let arrayLength = val.length;
          val.forEach((function (innerval, key1) {
            //var notEqual1 = this;
            //notEqual1 = innerval[0];
            if (typeof (innerval) === "string" && filterData.length > 0 && innerval !== '!')
              filterData[filterData.length - 1].LogicalOperator = innerval;
            else if (Array.isArray(innerval[key1]) === false && innerval.filterValue !== undefined && (typeof (innerval[0][0]) === "string" && innerval[0][0].indexOf("Date") === -1 && innerval[0][2] !== null))
              filterData.push({ ColumnName: innerval[0], Operator: ((notEqual === "!" || prevnotEqual === "!") && innerval[1] === "=" ? ("!" + innerval[1]) : innerval[1]), Value: innerval.filterValue, LogicalOperator: "" });
            else if (Array.isArray(innerval) === true && innerval.filterValue && (typeof (innerval[0][0]) === "string" && innerval[0][0].toLocaleLowerCase().indexOf("date") === -1))
              filterData.push({ ColumnName: innerval[0], Operator: innerval[1], Value: innerval.filterValue, LogicalOperator: "" });
            else if (Array.isArray(innerval) === true && typeof (innerval[0]) === "string" && innerval[0].toLocaleLowerCase().indexOf("date") >= 0) {
              filterData.push({ ColumnName: innerval[0], Operator: innerval[1], Value: innerval[2], LogicalOperator: "" });
            }
            else if (Array.isArray(innerval) === true && typeof (innerval[0]) === "string" && innerval[1] === "=" && (innerval[2] === null || innerval[2] === "")) {
              filterData.push({ ColumnName: innerval[0], Operator: ((notEqual === "!" || prevnotEqual === "!") && innerval[1] === "=" ? ("!" + innerval[1]) : innerval[1]), Value: (innerval[2] === null ? 'null' : ''), LogicalOperator: "" });
            }
            else if (Array.isArray(innerval) === true && Array.isArray(innerval[0]) === true) {
              innerval.forEach((function (minival, key2) {
                if (typeof (minival) === "string" && filterData.length > 0)
                  filterData[filterData.length - 1].LogicalOperator = minival;
                else if (Array.isArray(minival[key2]) === false && minival.filterValue !== undefined && (typeof (minival[0][0]) === "string" && minival[0][0].indexOf("Date") === -1 && minival[0][2] !== null))
                  filterData.push({ ColumnName: minival[0], Operator: ((notEqual === "!" || prevnotEqual === "!") && minival[1] === "=" ? ("!" + minival[1]) : minival[1]), Value: minival.filterValue, LogicalOperator: "" });
                else if (Array.isArray(minival) === true && typeof (minival[0]) === "string" && minival[0].toLocaleLowerCase().indexOf("date") >= 0) {
                  filterData.push({ ColumnName: minival[0], Operator: ((notEqual === "!" || prevnotEqual === "!") && minival[1] === "=" ? ("!" + minival[1]) : minival[1]), Value: minival[2], LogicalOperator: "" });
                }
                else if (Array.isArray(minival) === true && typeof (minival[0]) === "string" && minival[1] === "=" && (minival[2] === null || minival[2] === "")) {
                  filterData.push({ ColumnName: minival[0], Operator: ((notEqual === "!" || prevnotEqual === "!") && minival[1] === "=" ? ("!" + minival[1]) : minival[1]), Value: (minival[2] === null ? 'null' : ''), LogicalOperator: "" });
                }
              }).bind(this));
            }

          }).bind(this));
        }
        prevnotEqual = notEqual;
      }).bind(this));
    }
    if (filterData.length > 0) {
      //filterData.slice(filterData.findIndex(x => Array.isArray(x.ColumnName) == true));
      filterData[filterData.length - 1].LogicalOperator = "";
    }
    return filterData;
  }

  static CurrencyValueChanger(val: string) {
    var orig = val;
    val = val.toString().split(' ').join('');
    val = val.split(',').join('');
    val = val.toLowerCase();
    var str = val[val.length - 1];
    var otherStr = '';
    var num = '';
    if (isNullOrUndefined(val) || val == "" || val == "nan" || val == "NaN") {
      return "0".toString();
    }
    if (Number.isNaN(parseFloat(str))) {
      for (let i = 0; i < val.length - 1; i++) {
        if ((parseFloat(val[i]) >= 0 && parseFloat(val[i]) <= 9) || val[i] == '.') {
          num = num + val[i];
        }
        else {
          otherStr = otherStr + val[i];
        }
      }
      if (otherStr.length > 0) {
        return orig;
      }
      else {
        if (num == '') {
          return orig;
        }
        else if (str.length > 0) {
          if (str.includes('t')) {
            return CommonMethods.ThousandFormatter(parseFloat(num) * 1000);
          }
          else if (str.includes('k')) {
            return CommonMethods.ThousandFormatter(parseFloat(num) * 1000);
          }
          else if (str.includes('m')) {
            return CommonMethods.ThousandFormatter(parseFloat(num) * 1000000);
          }
          else if (str.includes('b')) {
            return CommonMethods.ThousandFormatter(parseFloat(num) * 1000000000);
          }
          else {
            return orig;
          }
        }
        else {
          return CommonMethods.ThousandFormatter(parseFloat(num));
        }
      }
    }
    else {
      for (let i = 0; i < val.length; i++) {
        if ((parseFloat(val[i]) >= 0 && parseFloat(val[i]) <= 9) || val[i] == '.') {
          num = num + val[i];
        }
        else {
          otherStr = otherStr + val[i];
        }
      }
      if (otherStr.length > 0) {
        return orig;
      }
      else {
        return CommonMethods.ThousandFormatter(parseFloat(num));
      }
    }
  }

  static maximize(name: string, cnrtlName: string, DxChart: QueryList<DxChartComponent>, DxPieChart: QueryList<DxPieChartComponent>, windowHeight: number) {
    //@ViewChildren('bChartCountOfAccTeams') bChartCountOfAccTeams!: QueryList<DxChartComponent>;
    //@ViewChildren(cnrtlName) Controls !: (cntrlType == "DxPie" ? QueryList<DxPieChartComponent> : QueryList<DxChartComponent>);
    if (DxChart)
      DxChart.forEach((element, index) => element._setOption("size", { height: (windowHeight - 300) }));
    if (DxPieChart)
      DxChart.forEach((element, index) => element._setOption("size", { height: (windowHeight - 300) }));

  }

  static ThousandFormatter(value: any) {
    if (value == null || value == undefined || value == "") {
      return "0".toString();
    }
    value = value.toString().split(",").join("");
    value = parseFloat(value.toString()).toString();
    var arr = value.split(".");
    if (arr[0].length < 4) {
      return value;
    }
    var triplets = arr[0].length / 3;

    var result = "";

    triplets = parseInt(triplets.toString());

    if (arr[0].length % 3 > 0) {
      result = arr[0].slice(0, arr[0].length % 3);
      result = result + ",";
    }

    arr[0] = arr[0].slice(arr[0].length % 3, arr[0].length);
    var num = 0;
    for (let i = 0; i < triplets; i++) {
      if ((num + 2) != arr[0].length - 1) {
        result = result + arr[0][num].toString() + arr[0][num + 1].toString() + arr[0][num + 2].toString() + ",";
        num = num + 3;
      }
      else {
        result = result + arr[0][num].toString() + arr[0][num + 1].toString() + arr[0][num + 2].toString();
      }
    }
    if (arr[1] && arr[1].length > 0) {
      return result + "." + arr[1].toString();
    }
    else {
      return result;
    }
  }

  static Includes(container, value) {
    var returnValue = false;
    var pos = container.indexOf(value);
    if (pos >= 0) {
      returnValue = true;
    }
    return returnValue;
  }

  static checkProperties(obj) {
    for (var key in obj) {
      if (obj[key] !== null && obj[key] != "" && obj[key] !== undefined)
        return false;
    }
    return true;
  }

  static Print(obj) {
    let printContents, popupWin, style, pageHeading;
    printContents = obj.innerHTML;
    if (obj.pageHeading) {
      pageHeading = '<h3 style="text-align:center;font-size: 18px;">' + obj.pageHeading + '</h3><hr/>';
    }

    popupWin = window.open('', '_blank', 'top=0,left=0,height=80%,width=auto');
    popupWin.document.open();
    style = '<style>@media print {  h3{color:#0e3790; font-size: 16px; font-weight: 600;} .printhidesection, .printhidesection * { display: none; } '
      + ' ul { list-style-type: none; content: "\A"; white-space: pre; } '
      + ' div.row > div { display: inline-block; margin: 0.2cm; } .col-md-12, .col-lg-12 {flex: 0 0 100 %;max-width: 100 %;} '
      + ' .table-responsive { display: block; width: 100 %;overflow-x: auto; } '
      + ' .table {width: 100 %; margin-bottom: 1rem; color: #6B6F82; border-collapse: collapse; } '
      + ' .table td { padding: 0.50rem 1.0rem; font-size: 13px; }'
      + ' .table-bordered td {border: 1px solid #E3EBF3;} '
      + ' .table th{padding:.5rem 1rem;color:#222;font-weight:500;font-size:13px;background-color:gray} '
      + ' .table thead th {vertical-align: bottom;border: 2px solid #E3EBF3;} .dx-datagrid-table{position: relative; border: 1px solid #eee;border-collapse: collapse; table-layout: fixed;}'
      + ' .dx-datagrid-table td{ border-right: 1px solid #eee;border-collapse: collapse;}'
    if (obj.loadCSS) {
      style = style + obj.loadCSS;
    }
    style = style + ' } '
      + ' </style>';
    //<html><head><link rel="stylesheet" type="text/css" media="print" href="src/styles.css" /></head><body onload="window.print();window.close()">${printContents}</body></html>`
    popupWin.document.write(`
      <html><head></head>
      ${style}
      <body onload="window.print();window.close()">${pageHeading}${printContents}</body></html>`
    );
    popupWin.document.close();
  }

  // for getting the numeric value out of string like "$123,123,123"
  static GetCurrencyValue(val: any) {
    var result = "";
    if (!isNullOrUndefined(val)) {
      for (let chr of val.toString()) {
        if (parseInt(chr) != NaN && parseInt(chr) >= 0 && parseInt(chr) <= 9) {
          result = result + chr;
        }
      }
    }
    else {
      result = "0";
    }
    return result;
  }

  // can be used as string.replaceAll()
  static ReplaceAll(dataString: string, searchChar: string, replaceChar: string) {
    return dataString.split(searchChar).join(replaceChar);
  }

  static removeLastComma(strng) {
    if (strng.lastIndexOf(",") >= 0)
      strng = strng.substring(0, strng.lastIndexOf(","))
    return strng;
  }
}



function isNullOrUndefined(val: string) {
  return val === null || val === undefined;
}
