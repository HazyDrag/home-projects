<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebChat.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 164px;
            height: 409px;
        }
        .auto-style2 {
            width: 641px;
            height: 409px;
        }
        .auto-style3 {
            width: 164px;
            height: 46px;
        }
        .auto-style4 {
            width: 641px;
            height: 46px;
        }
        .auto-style5 {
            height: 46px;
        }
        .auto-style9 {
            height: 409px;
        }
        .auto-style10 {
            width: 164px;
            height: 25px;
        }
        .auto-style11 {
            width: 641px;
            height: 25px;
        }
        .auto-style12 {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 510px">
    
        <table style="width: 100%; height: 155px;">
            <tr>
                <td class="auto-style10">
                    <asp:Label ID="Label1" runat="server" Text="Введите свое имя! "></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:TextBox ID="TextBox1" runat="server" Width="125px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Height="22px" OnClick="Button1_Click" Text="Войти" Width="64px" style="margin-left: 0px" />
                </td>
                <td class="auto-style12">
                    <asp:Button ID="Button3" runat="server" Height="36px" OnClick="Button3_Click" Text="Выход" Width="154px" Visible="False" />
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="5000" OnTick="Timer1_Tick1">
                    </asp:Timer>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListBox ID="ListBox1" runat="server" Height="386px" Visible="False" Width="139px"></asp:ListBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:ListBox ID="ListBox2" runat="server" Height="393px" Width="635px" Visible="False"></asp:ListBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="Label3" runat="server" Text="Каталог файлов:" Visible="False"></asp:Label>
                            <br />
                    <asp:Repeater ID="Repeater1" runat="server" OnPreRender="Repeater1_PreRender" Visible="False">
                        <ItemTemplate>                            
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# "~/Uploads/" + Eval("Name") %>' />
                       </ItemTemplate>
                        <SeparatorTemplate>
                             <br />
                        </SeparatorTemplate>
                    </asp:Repeater>
                    </td>
            </tr>
            <tr>
                <td class="auto-style3">
                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged" Visible="False" Width="519px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Отправить" Visible="False" Width="103px" />
                </td>
                <td class="auto-style5">
                    <asp:FileUpload ID="FileUpload1" runat="server" Load="FileUpload1_Load" Visible="False" />
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Загрузить файл" Visible="False" />
                </td>
            </tr>
        </table>
    
    </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
