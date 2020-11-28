Imports System.IO
Imports System.Xml

Module Module1
    Public ItemsDictionary As New Dictionary(Of String, String)
    Public SelectItem As String
    Public downloadPath As String
    Public Sub WriteXML2(Maven As String, outPath As String)
        Using writer As New XmlTextWriter(outPath, System.Text.Encoding.UTF8)
            writer.WriteStartDocument()
            writer.Formatting = Formatting.Indented
            writer.Indentation = 4
            writer.WriteStartElement("project", Nothing)
            writer.WriteAttributeString("xmlns", "http://maven.apache.org/POM/4.0.0")
            writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
            writer.WriteAttributeString("xsi", "schemaLocation", Nothing, "http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd")

            writer.WriteStartElement("modelVersion")
            writer.WriteString("4.0")
            writer.WriteEndElement()

            writer.WriteStartElement("groupId")
            writer.WriteString("test.download")
            writer.WriteEndElement()

            writer.WriteStartElement("artifactId")
            writer.WriteString("test.download")
            writer.WriteEndElement()

            writer.WriteStartElement("version")
            writer.WriteString("1.0.0")
            writer.WriteEndElement()
            writer.WriteStartElement("dependencies")

            Using stringReader As New StringReader(Maven)
                Using xmlReader As XmlReader = System.Xml.XmlReader.Create(stringReader)
                    writer.WriteNode(xmlReader, False)
                End Using
            End Using

            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using
    End Sub
End Module
