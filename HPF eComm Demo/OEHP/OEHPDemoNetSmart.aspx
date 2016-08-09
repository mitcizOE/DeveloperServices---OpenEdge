<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OEHPDemoNetSmart.aspx.cs" Inherits="HostPayFunctions.OEHPDemoNetSmart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script lang="Javascript">
       <!-
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
        .auto-style3 {
            width: 347px;
        }
    </style>
</head>
<body>
    
<img src="https://www.ntst.com/images/ntst-logo-white.png" class="auto-style3" />
    <form id="form1" runat="server">
        <div>

            <br />

            <br />
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1">Customer Type:<br />
            <asp:DropDownList ID="CustomerTypeDropDown" runat="server">
                <asp:ListItem Text="Walk In" Value="AUTO" />
                <asp:ListItem Text="Phone" Value="KEYED" />
                <asp:ListItem Text="EMV" Value="EMV" />
            </asp:DropDownList>
            <br />
            Charge Type:<br />
            <asp:DropDownList ID="ChargeTypeDropDown" runat="server">
                <asp:ListItem Text="Sale" Value="SALE" />
                <asp:ListItem Text="Refund" Value="CREDIT" />
            </asp:DropDownList>
            <br />
            Amount:<br />
            <asp:TextBox ID="AmountBox" runat="server" OnTextChanged="AmountBox_TextChanged" Width="80px"></asp:TextBox>
            <br />
            <br />

            <asp:Button ID="SubmitButton" runat="server" BackColor="#0044772" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="37px" OnClick="SubmitButton_Click" Text="Enter Card Information" Width="160px" BorderWidth="5px" />

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