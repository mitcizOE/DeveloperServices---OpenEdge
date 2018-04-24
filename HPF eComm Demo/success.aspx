<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="success.aspx.cs" Inherits="HPF_eComm_Demo.success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Congratulations! You have successfully completed a transaction!</h2>
        <asp:Button ID="ButtonRedirect" runat="server" Text="Pay More" OnClick="ButtonRedirect_Click" />
    </div>
    </form>
</body>
</html>
