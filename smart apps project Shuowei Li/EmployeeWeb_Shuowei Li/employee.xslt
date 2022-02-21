<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"  xmlns:m= "https://www.shuowei.org/Employees"
>
   <xsl:template match="/">
	   <html>
		   <body>
			   <h2>Employees Data</h2>
			   <table border="1">
				   <tr>
					   <th>id</th>
					   <th>name</th>
					   <th>phone</th>
					   <th>department</th>
					   <th>street</th>
					   <th>city</th>
					   <th>state</th>
					   <th>zip</th>
					   <th>country</th>
				   </tr>
				   <xsl:for-each select="m:employees/m:employee">
					   <tr>
						   <td>
							   <xsl:value-of select ="m:id"/>
						   </td>
						   <td>
							   <xsl:value-of select ="m:name"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:phone"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:department"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:street"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:city"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:state"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:zip"/>
						   </td>
						   <td>
						   <xsl:value-of select ="m:country"/>
						   </td>
					   </tr>
				   </xsl:for-each>
			   </table>
		   </body>
	   </html>
        
    </xsl:template>
</xsl:stylesheet>
