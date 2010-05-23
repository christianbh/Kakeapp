<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#newDate").datepicker({ dateFormat: 'dd.mm.yy' }); ;
        }); 
    </script>   
    <h2>Ny feriedag</h2>
    <%
        using (Html.BeginForm())
        {%>
            <div class="editor-label">
               Ny feriedato
            </div>
            <div class="editor-field">
                <input type="text" size="10" value="velg dato" name="newDate" id="newDate"/>
            </div>
            
             <p>
                <input type="submit" value="Legg til" />
            </p>
    <%
        }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
