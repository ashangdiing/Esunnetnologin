<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Esunnet.Record.Test" %>

<html xmlns="http://www.w3.org/1999/xhtml" lang="chs" xml:lang="chs">
<head id="Head1" runat="server">
    <title>软电话</title>
    <link rel="stylesheet" href="SoftPhone.css" charset="UTF-8" />
    <style type="text/css">
        .InputTextYellow
        {
            border-right: 1px solid #94C7E7;
            border-top: 1px solid #94C7E7;
            border-left: 1px solid #94C7E7;
            border-bottom: 1px solid #94C7E7;
            font-size: 9pt;
            vertical-align: middle;
            height: 20px;
            padding: 2px;
            background-color: White;
            background-color: #ffffbc;
        }
        #Button10
        {
            width: 40px;
        }
        #txtlog
        {
            height: 116px;
            width: 987px;
        }
    </style>
    <script type="text/javascript" language="javascript">
 
        function unlock() {
            document.getElementById("lock").readOnly = false;
        }

        function locked() {
           // alert(document.getElementById("lock").value);
            document.getElementById("lock").readOnly = true;
        }


    </script>

    
</head>




<body>
    <form id="a">
        测试文本<input type="text" value="文本" id="lock" />
          <input type="button" value="锁住" onclick="locked()"/>
        <input type="button" value="解锁" onclick="unlock()"/>
    </form>
    </body>
</html>
