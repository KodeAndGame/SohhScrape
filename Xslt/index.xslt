<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <table class="list" width="100%" cellpadding="0" cellspacing="0" border="0">
      <xsl:for-each select=".//tbody[@id='collapseobj_forumbit_22']//td[contains(@id, 'f')]">
            <tr>
              <td width="100%">
                <a href="{./div/a/@href}" class="threadItem">
                  <xsl:value-of select="./div/a"/>
                </a>
              </td>
              <td>
                <a href="{@href}" class="threadItemArrow"></a>
              </td>
            </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>