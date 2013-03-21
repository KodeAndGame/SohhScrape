<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showthread.aspx.cs" Inherits="SohhScrape.showthread" %>

<!DOCTYPE html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mobile SOHH</title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;" />
    <link rel="icon" type="image/vnd.microsoft.icon" href="images/favicon.ico" />
    <link rel="apple-touch-icon" href="images/apple-touch-icon.png" />
    <style type="text/css">
	    @import "images/styleMainV28.css";
	    body { font-size: 100%; }
    </style>
    <script type="text/javascript" src="scripts/functions28.js"></script>
</head>
<body id="Body1" runat="server">
    <div id="bodyThread">
        <!--BEGIN HEADER-->
        <a name="top"></a>
        <div id="headerToolbar" class="toolbar">
            <h1>
                <a href="<%="showthreadPosters.aspx?t="%>"><%=ThreadTitle %></a></h1>
            <a class="backButton" href="<%="forumdisplay.aspx?f=" + ForumId%>"><span>Forum</span></a>
            <%if (MaxPages > 1 && CurrentPage <= MaxPages / 2) { %>
            <a class="rightButton" href="<%="showthread.aspx?t=" + Request.QueryString["t"] + "&page=" + (MaxPages)%>"><span>
                Last Page</span></a>
            <%}
              else if (MaxPages > 1 && CurrentPage > MaxPages / 2)
              { %>
            <a class="rightButton" href="<%="showthread.aspx?t=" + Request.QueryString["t"] + "&page=1"%>"><span>
                First Page</span></a>
            <%} %>
        </div>
        <div id="subHeader">
            <a href="#bottom" onclick="window.scrollTo(0, 100000); return false;">
                <%=ThreadTitle %><br />
                <span class="pageNumbers">
                    <%=PageCountString %></span></a>
        </div>
        <!--here-->
        <asp:Literal ID="PostContent" runat="server"></asp:Literal>
        <div id="newposts">
        </div>
        <div id="controls">
            <div class="buttonGroup" id="bottomNav">
                <%if (CurrentPage == 1)
                  { %>
                <div class="roundButton">
                    Prev Page</div>
                <%}
                  else
                  {%>
                <a class="roundButton" href="showthread.aspx?t=<%=ThreadId.ToString() + "&amp;page=" + (CurrentPage - 1).ToString()%>">
                    Prev Page</a>
                <%} %>
                <%if (CurrentPage == MaxPages)
                  { %>
                <!-- <div class="roundButton">Next Page</div> -->
                <a class="roundButton" href="#" id="refresh" onclick="showthreadUpdate('showthreadUpdate.aspx?t=<%=ThreadId.ToString() %>&amp;page=<%=MaxPages%>&amp;lastPost=<%=RunningPostCount.ToString() %>'); return false;">Refresh</a>
                <%}
                  else
                  {%>
                <a class="roundButton" href="showthread.aspx?t=<%=ThreadId.ToString() + "&amp;page=" + (CurrentPage + 1).ToString()%>">
                    Next Page</a>
                <%}%>
            </div>
        </div>
        <div id="subFooter">
            <%if (Session["username"] != String.Empty) { %>
                <a href="#top" onclick="window.scrollTo(0, 1); return false;"><%=Session["username"] %></a>
            <%} else { %>
                <a href="#top" onclick="window.scrollTo(0, 1); return false;">Not Logged In</a>
            <%} %>
        </div>
        <a name="bottom"></a>
        <div id="footerToolbar" class="toolbar">
            <a class="centerButton" href="#" onclick="<%="gotoPage('thread', " + ThreadId + ", " + MaxPages + "); return false;"%>">
                <span><%=PageCountString%></span>
            </a> 
            <a class="backButton" href="<%="forumdisplay.aspx?f=" + ForumId%>"><span>Forum</span></a> 
            <%if (Session["username"] == null || Session["username"] == String.Empty) { %>
                <a class="rightButton" href="http://forums.projectcovo.com/newreply.php?do=newreply&noquote=1&p=<%=LastPostId%>" target="reply"><span>Reply</span></a>
            <%} else { %>
                <a class="rightButton" href="post.aspx?do=newreply&noquote=1&p=<%=LastPostId%>"><span>Reply</span></a>
            <%} %>
            
        </div>
    </div>
</body>
</html>
