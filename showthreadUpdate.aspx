<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showthread.aspx.cs" Inherits="SohhScrape.showthread" %>

<asp:Literal ID="PostContent" runat="server"></asp:Literal><%if (RefreshContainsData)
  { %>
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
<%} %>