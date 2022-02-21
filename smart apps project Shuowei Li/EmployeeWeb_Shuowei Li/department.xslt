<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"  xmlns:m= "https://www.shuowei.org/departments"
>
	<xsl:template match="/">
		<html>
			<body>
				<h2>Departments Data</h2>
				<table border="1">
					<tr>
						<th>id</th>
						<th>name</th>
						
					</tr>
					<xsl:for-each select="m:departments/m:department">
						<tr>
							<td>
								<xsl:value-of select ="m:id"/>
							</td>
							<td>
								<xsl:value-of select ="m:name"/>
							</td>
							
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>

	</xsl:template>
</xsl:stylesheet>
