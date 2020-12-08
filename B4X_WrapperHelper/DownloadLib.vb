Imports System.Net
Imports System.Web
Imports HtmlAgilityPack

Public Class DownloadLib
    Public Shared mycookiecontainer As CookieContainer = New CookieContainer()
    Private Shared UrlReferer = "https://mvnrepository.com/"
    Private Shared redirecturl As String = ""
    Public Shared Async Function SearchItem(name As String) As Task(Of Dictionary(Of String, String))
        Dim ItemDictionary As New Dictionary(Of String, String)
        Dim Headerdics As New Dictionary(Of String, Object) From
        {
            {"Connection", "keep-alive"},
            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.51"},
            {"Referer", "https://mvnrepository.com/"}
        }
        Dim url = "https://mvnrepository.com/search?q=" + HttpUtility.UrlEncode(name)
        UrlReferer = url
        Dim Res = HttpHelper.HttpClientGetAsync(url, Headerdics, mycookiecontainer, redirecturl)
        If Await Res <> "" Then
            Dim doc As New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(Res.Result)
            Try
                Dim i = 0
                For Each Node As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div//h2[@class='im-title']")
                    i += 1
                    Dim title = Node.SelectNodes(".//a").Select(Function(x) x.InnerText.Trim())(0)
                    Dim link = Node.SelectNodes(".//a").Select(Function(x) x.GetAttributeValue("href", ""))(0)
                    If ItemDictionary.ContainsKey(title.Trim) = False Then
                        ItemDictionary.Add(title.Trim, link.Trim)
                    Else
                        ItemDictionary.Add(title.Trim + "_" + i.ToString, link.Trim)
                    End If
                Next
            Catch ex As Exception

            End Try
        End If

        UrlReferer = url
        url = "https://mvnrepository.com/search?q=" + name + "&p=2"
        Res = HttpHelper.HttpClientGetAsync(url, Headerdics, mycookiecontainer, redirecturl)
        If Await Res <> "" Then
            Dim doc As New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(Res.Result)
            Try
                Dim i = 0
                For Each Node As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div//h2[@class='im-title']")
                    i += 1
                    Dim title = Node.SelectNodes(".//a").Select(Function(x) x.InnerText.Trim())(0)
                    Dim link = Node.SelectNodes(".//a").Select(Function(x) x.GetAttributeValue("href", ""))(0)
                    If ItemDictionary.ContainsKey(title.Trim) = False Then
                        ItemDictionary.Add(title.Trim, link.Trim)
                    Else
                        ItemDictionary.Add(title.Trim + "_" + i.ToString, link.Trim)
                    End If
                Next
            Catch ex As Exception

            End Try
        End If

        Return ItemDictionary
    End Function
    Public Shared Async Function GetVersion(url As String) As Task(Of Dictionary(Of String, String))
        Dim ItemDictionary As New Dictionary(Of String, String)
        Dim Headerdics As New Dictionary(Of String, Object) From
        {
            {"Connection", "keep-alive"},
            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.51"},
            {"Referer", UrlReferer}
        }
        url = "https://mvnrepository.com" + url
        Dim Res = HttpHelper.HttpClientGetAsync(url, Headerdics, mycookiecontainer, redirecturl)
        If Await Res <> "" Then
            Dim doc As New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(Res.Result)
            Try
                Dim i = 0
                For Each Node As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div//table[@class='grid versions']//tbody//tr")
                    Dim title = Node.SelectNodes(".//td//a").Select(Function(x) x.InnerText.Trim())(0)
                    Dim link = Node.SelectNodes(".//td//a").Select(Function(x) x.GetAttributeValue("href", ""))(0)
                    If ItemDictionary.ContainsKey(title.Trim) = False Then
                        ItemDictionary.Add(title.Trim, link.Trim)
                    Else
                        ItemDictionary.Add(title.Trim + "_" + i.tostring, link.Trim)
                    End If
                Next
            Catch ex As Exception

            End Try
        End If
        UrlReferer = url
        Return ItemDictionary
    End Function
    Public Shared Async Function GetPomFile(url As String) As Task(Of Dictionary(Of String, String))
        Dim ItemDictionary As New Dictionary(Of String, String)
        Dim Headerdics As New Dictionary(Of String, Object) From
        {
            {"Connection", "keep-alive"},
            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.51"},
            {"Referer", UrlReferer}
        }
        url = "https://mvnrepository.com" + url
        Dim Res = HttpHelper.HttpClientGetAsync(url, Headerdics, mycookiecontainer, redirecturl)
        If Await Res <> "" Then
            Dim doc As New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(Res.Result)
            For Each Node As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//div[@id='maincontent']//table[@class='grid']//tr")
                Try
                    For Each subNode As HtmlNode In Node.SelectNodes("//a[contains(@class, 'vbtn')]")
                        Dim title = subNode.InnerText
                        Dim link = subNode.GetAttributeValue("href", "")
                        ItemDictionary.Add(title, link)
                    Next
                Catch ex As Exception

                End Try
            Next
        End If
        Return ItemDictionary
    End Function

    Public Shared Async Function DownloadLibFile(url As String) As Task(Of String)
        Dim Headerdics As New Dictionary(Of String, Object) From
        {
            {"Connection", "keep-alive"},
            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.51"},
            {"Referer", UrlReferer}
        }
        url = "https://mvnrepository.com" + url
        Dim Res = HttpHelper.HttpClientGetAsync(url, Headerdics, mycookiecontainer, redirecturl)
        If Await Res <> "" Then
            Dim doc As New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(Res.Result)
            Try
                For Each Node As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//textarea[@id='maven-a']")
                    Return HttpUtility.HtmlDecode(Node.InnerText)
                Next
            Catch ex As Exception

            End Try
        End If
        Return ""
    End Function
    Public Shared Async Function GetMaven(url As String) As Task(Of String)
        Dim Headerdics As New Dictionary(Of String, Object) From
        {
            {"Connection", "keep-alive"},
            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36 Edg/86.0.622.51"},
            {"Referer", UrlReferer}
        }
        url = "https://mvnrepository.com" + url
        Dim Res = HttpHelper.HttpClientGetAsync(url, Headerdics, mycookiecontainer, redirecturl)
        If Await Res <> "" Then
            Dim doc As New HtmlAgilityPack.HtmlDocument
            doc.LoadHtml(Res.Result)
            Try
                For Each Node As HtmlAgilityPack.HtmlNode In doc.DocumentNode.SelectNodes("//textarea[@id='maven-a']")
                    Return HttpUtility.HtmlDecode(Node.InnerText)
                Next
            Catch ex As Exception

            End Try
        End If
        Return ""
    End Function
End Class
