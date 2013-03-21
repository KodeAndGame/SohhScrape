<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forumdisplay.aspx.cs" Inherits="SohhScrape.forumdisplay"  MasterPageFile="~/MobileSohhMaster.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="preXsltContent" runat="server">
    <div id="bodyForum">
        <a name="top"></a>
        <div id="headerToolbar" class="toolbar">
            <h1><a href="#" onclick="jumpFromForum(<%=ForumId.ToString() %>); return false;"><%=ForumTitle %></a></h1>
            <a class="backButton" href="index.aspx"><span>Main</span></a> 
            <!-- <a class="rightButton" href="#" onclick="searchPrompt(''); return false;"><span>Search</span></a> -->
            <%if (Session["username"] == null || Session["username"] == String.Empty) { %>
                <a class="rightButton" href="http://forums.projectcovo.com/newthread.php?do=newthread&f=<%=ForumId%>" target="newtopic"><span>New Topic</span></a>
            <%} else { %>
                <a class="rightButton" href="post.aspx?do=newthread&f=<%=ForumId%>"><span>New Topic</span></a>
            <%} %>
        </div>
        <div id="subHeader">
            <a href="#bottom" onclick="window.scrollTo(0, 100000); return false;">
                <%=ForumTitle%><br>
                <span class="pageNumbers">
                    <%=PageCountString %></span></a>
        </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="postXsltContent" runat="server">
        <div class="buttonGroup">
            <%if (CurrentPage == 1)
              { %>
            <div class="roundButton">
                Prev Page</div>
            <%}
              else
              {%>
              <a class="roundButton" href="forumdisplay.aspx?f=<%=ForumId.ToString() + "&amp;page=" + (CurrentPage - 1).ToString()%>">
                Prev Page</a>
            <%} %>



            <%if (CurrentPage == MaxPages)
              { %>
            <div class="roundButton">
                Next Page</div>
            <%}
              else
              {%>
            <a class="roundButton" href="forumdisplay.aspx?f=<%=ForumId.ToString() + "&amp;page=" + (CurrentPage + 1).ToString()%>">
                Next Page</a>
            <%}%>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerBarContent" runat="server">
        <div id="footerToolbar" class="toolbar">
            <a class="centerButton" href="#" onclick="<%= "gotoPage('forum', " + ForumId + ", " + MaxPages + "); return false;" %>">
                <span>
                    <%=PageCountString%></span></a> <a class="backButton" href="index.aspx"><span>Main</span></a>
                    
                    <%if (Session["username"] == null || Session["username"] == String.Empty) { %>
                        <a class="rightButton" href="http://forums.projectcovo.com/newthread.php?do=newthread&f=<%=ForumId%>" target="newtopic"><span>New Topic</span></a>
                    <%} else { %>
                        <a class="rightButton" href="post.aspx?do=newthread&f=<%=ForumId%>"><span>New Topic</span></a>
                    <%} %>
        </div>
    </div>
</asp:Content>
