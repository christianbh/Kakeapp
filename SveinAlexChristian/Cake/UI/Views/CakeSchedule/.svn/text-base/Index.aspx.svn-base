<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Cake.Core.Domain.CakeSchedule>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Kake
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th>Feriedager</th>
        </tr>
    <%
        foreach (DateTime holiday in ViewData.Model.Holidays.Where(h => h.Date > DateTime.Now).OrderBy(h => h.Date))
        {%>
        <tr>
            <td><%:holiday.ToShortDateString()%></td>
            <td><%:Html.ActionLink("Endre", "Edit", new {id = holiday.ToShortDateString()})%></td>
            <td><%:Html.ActionLink("Slett", "Delete", new {id = holiday.ToShortDateString()})%></td>
        </tr>
    <%
        }%>
        <tr>
            <td><%:Html.ActionLink("Legg til", "Create")%></td>
        </tr>
    </table>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
