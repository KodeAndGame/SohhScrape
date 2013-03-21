<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <table class="list" width="100%" cellpadding="0" cellspacing="0" border="0">
      <xsl:variable name="CountUppedThreads" select="count(//td[contains(@id, 'td_threadtitle')]/div/b[contains(., 'Upped:')])"/>

      <xsl:if test="$CountUppedThreads &gt; 0">
        <tr>
          <td colspan="2" class="separator">
            Upped
          </td>
        </tr>
      </xsl:if>
      <xsl:for-each select="//td[contains(@id, 'td_threadtitle')]">
        <xsl:if test="position() = $CountUppedThreads + 1">
          <tr>
            <td colspan="2" class="separator">
              &#160;
            </td>
          </tr>
        </xsl:if>
        <tr>
          <td width="100%">
            <a class="threadItem">
              <xsl:attribute name="href">
                <xsl:value-of select=".//a[contains(@id, 'thread_title_')]/@href"/>
              </xsl:attribute>
              <xsl:value-of select=".//a[contains(@id, 'thread_title_')]/."/>
              <br/>
              <span class="totalPages">
                <xsl:value-of
                  select="floor(translate(string(..//td/a[contains(@href, 'misc.php')]), ',', '') div 15) + 1"/>
                <xsl:choose>
                  <xsl:when
                  test="(floor(translate(string(..//td/a[contains(@href, 'misc.php')]), ',', '') div 15) + 1) > 1">
                    pages
                  </xsl:when>
                  <xsl:otherwise>
                    page
                  </xsl:otherwise>
                </xsl:choose>
                -
                <xsl:value-of select="..//td/a[contains(@href, 'misc.php')]"/>
                <xsl:choose>
                  <xsl:when test="(number(..//td/a[contains(@href, 'misc.php')])) = 1">
                    reply
                  </xsl:when>
                  <xsl:otherwise>
                    replies
                  </xsl:otherwise>
                </xsl:choose>
                - Last Updated: 
                <xsl:value-of select="..//td[contains(@title, 'Replies:')]/div"/>
              </span>
            </a>
          </td>
          <td>
            <a href="{concat(.//a[contains(@id, 'thread_title_')]/@href, '&amp;goto=newpost')}" class="threadItemArrow"></a>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>