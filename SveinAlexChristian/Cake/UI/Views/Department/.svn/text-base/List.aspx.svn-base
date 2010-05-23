<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Cake.Core.Domain.Department>>" %>
<%@ Import Namespace="Cake.Core.Domain" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Avdelinger</h2>

    <table>
        <tr>
            <th>
                <%:Html.LabelFor(model => model.First().ContactName)%>
            </th>
            <th>
                <%:Html.LabelFor(model => model.First().ContactEmail)%>
            </th>
        </tr>

    <%
        foreach (Department item in Model)
        {%>
    
        <tr>
            <td>
                <%:item.ContactName%>
            </td>
            <td>
                <%:item.ContactEmail%>
            </td>
            <td>
                <%:Html.ActionLink("Endre", "Edit", new {id = item.Id})%> |
                <%:Html.ActionLink("Slett", "Delete", new {id = item.Id})%>
            </td>
        </tr>
    
    <%
        }%>

    </table>

    <p>
        <%:Html.ActionLink("Ny kontaktperson", "Create")%>
    </p>

</asp:Content>


