<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OEHPDemo.aspx.cs" Inherits="HostPayFunctions.OEHPDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script lang="Javascript">
       <!--
       function isNumberKey(evt)
       {
          var charCode = (evt.which) ? evt.which : event.keyCode
          if (charCode != 46 && charCode > 31 
            && (charCode < 48 || charCode > 57))
             return false;
 
          return true;
       }
       //-->
    </script>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 185px;
            vertical-align: top;
        }
        .auto-style2 {
            height: 528px;
        }
        .headerClass {
            background-color: grey;
        }
    </style>
</head>
<body>
    <div class="headerClass">
        &nbsp;<img src="http://www.evosus.com/static/website/css/images/logo.png" class="auto-style3" />
    </div>


    <form id="form1" runat="server">
        <div>

            <br />

            <br />
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1">
                        Enter Payment
            Amount:<br />
            <asp:TextBox ID="AmountBox" runat="server" OnTextChanged="AmountBox_TextChanged" Width="80px"></asp:TextBox>
                        <br />
                        Service Invoice #:<br />
                        <asp:TextBox ID="ServiceNumberBox" runat="server" OnTextChanged="ServiceNumberBox_TextChanged"></asp:TextBox>
                        <br />
            <br />

            <asp:Button ID="SubmitButton" runat="server" BackColor="Gray" BorderStyle="None" Font-Bold="True" Height="37px" OnClick="SubmitButton_Click" Text="Enter Card Information" Width="160px" BorderWidth="5px" ForeColor="White" />

                        <br />

                    </td>
                    <td style="vertical-align: top;">
                    </tr>
            </table>
            <br />

        </div>
    <div>
        <asp:scriptmanager ID="defaultScriptManager" runat="server"></asp:scriptmanager>
        <iframe id="oehpIFrame" runat="server" scrolling="no" style="border-style: hidden; " class="auto-style2" on/> </td>
        
    </div>
    </form>
</body>
</html>
<%--  --%>