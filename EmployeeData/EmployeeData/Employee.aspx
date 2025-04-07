<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="EmployeeData.EmployeeData.Employee" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Employee Management</title>
    <script type="text/javascript">
        function clearForm() {
            document.getElementById('<%= txtEmployeeId.ClientID %>').value = '';
            document.getElementById('<%= txtName.ClientID %>').value = '';
            document.getElementById('<%= txtPosition.ClientID %>').value = '';
            document.getElementById('<%= txtDepartment.ClientID %>').value = '';
            document.getElementById('<%= btnSave.ClientID %>').value = 'Add Employee';
        }

        function editEmployee(id, name, position, department) {
            document.getElementById('<%= txtEmployeeId.ClientID %>').value = id;
            document.getElementById('<%= txtName.ClientID %>').value = name;
            document.getElementById('<%= txtPosition.ClientID %>').value = position;
            document.getElementById('<%= txtDepartment.ClientID %>').value = department;
            document.getElementById('<%= btnSave.ClientID %>').value = 'Update Employee';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Employee Management</h2>

            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

            <asp:Panel ID="pnlEmployeeForm" runat="server">
                <asp:Label ID="lblEmployeeId" runat="server" Text="Employee ID" Visible="false"></asp:Label>
                <asp:TextBox ID="txtEmployeeId" runat="server" Visible="false"></asp:TextBox><br />

                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />

                <asp:Label ID="lblPosition" runat="server" Text="Position:"></asp:Label>
                <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox><br />

                <asp:Label ID="lblDepartment" runat="server" Text="Department:"></asp:Label>
                <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox><br />

                <asp:Button ID="btnSave" runat="server" Text="Add Employee" OnClick="btnSave_Click" /><br />
            </asp:Panel>

            <h2>Employee List</h2>
            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" OnRowCommand="gvEmployees_RowCommand">
                <Columns>
                    <asp:BoundField DataField="EmployeeId" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Position" HeaderText="Position" />
                    <asp:BoundField DataField="Department" HeaderText="Department" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditEmployee" CommandArgument='<%# Eval("EmployeeId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
