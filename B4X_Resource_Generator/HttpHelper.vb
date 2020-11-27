'https://github.com/laomms

Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Authentication
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Reflection
Imports System.Net.Http
Imports System.IO.Compression
Imports BrotliSharpLib

Module HttpHelper
    Private RestrictedHeaders() As String = {"Accept", "Connection", "Content-Length", "Content-Type", "Date", "Expect", "Host", "If-Modified-Since", "Keep-Alive", "Proxy-Connection", "Range", "Referer", "Transfer-Encoding", "User-Agent"}

    Public Function ListCookie(ByVal container As CookieContainer) As List(Of Cookie)
        Dim cookies = New List(Of Cookie)

        Dim table = DirectCast(container.GetType().InvokeMember("m_domainTable", BindingFlags.NonPublic Or BindingFlags.GetField Or BindingFlags.Instance, Nothing, container, Nothing), Hashtable)

        For Each key As String In table.Keys
            Dim item = table(key)
            Dim items = DirectCast(item.GetType().GetProperty("Values").GetGetMethod().Invoke(item, Nothing), ICollection)
            For Each cc As CookieCollection In items
                For Each cookie As Cookie In cc
                    Debug.Print(cookie.ToString + "  " + key.ToString)
                    cookies.Add(cookie)
                Next cookie
            Next cc
        Next key

        Return cookies
    End Function

    ''' <summary>
    ''' Http请求
    ''' </summary>
    ''' <param name="url">请求网址</param>
    ''' <param name="Headerdics">头文件固定KEY值字典类型泛型集合</param>
    ''' <param name="heard">头文件集合</param>
    ''' <param name="cookieContainers">cookie容器</param>
    ''' <param name="redirecturl">头文件中的跳转链接</param>
    ''' <returns>返回请求字符串结果</returns>
    Public Function RequestGet(ByVal url As String, Headerdics As Dictionary(Of String, Object), ByVal heard As WebHeaderCollection, ByRef cookieContainers As CookieContainer, ByRef redirecturl As String) As String
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function

        'Dim domain = CStr(Regex.Match(url, "^(?:\w+://)?([^/?]*)").Groups(1).Value)

        'If domain.Contains("www.") = True Then
        '    domain = domain.Replace("www.", "")
        'Else
        '    domain = "." & domain
        'End If
        If url = "" Then Return ""
        Dim myRequest As HttpWebRequest = WebRequest.Create(url)
        myRequest.Headers = heard
        myRequest.Method = "GET"
        For Each pair In Headerdics
            GetType(HttpWebRequest).GetProperty(pair.Key).SetValue(myRequest, pair.Value, Nothing)
        Next
        myRequest.CookieContainer = cookieContainers
        Dim results As String = ""

        Try
            Using myResponse As HttpWebResponse = myRequest.GetResponse()
                If myResponse.ContentEncoding.ToLower().Contains("gzip") Then
                    Using stream As Stream = New System.IO.Compression.GZipStream(myResponse.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress)
                        Using reader = New StreamReader(stream)
                            results = reader.ReadToEnd()
                        End Using
                    End Using
                ElseIf myResponse.ContentEncoding.ToLower().Contains("deflate") Then
                    Using stream As Stream = New System.IO.Compression.DeflateStream(myResponse.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress)
                        Using reader = New StreamReader(stream)
                            results = reader.ReadToEnd()
                        End Using
                    End Using
                Else
                    Using stream As Stream = myResponse.GetResponseStream()
                        Using reader = New StreamReader(stream, Encoding.UTF8)
                            results = reader.ReadToEnd()
                        End Using
                    End Using
                End If
                If myResponse.Headers("Location") IsNot Nothing Then
                    redirecturl = myResponse.Headers("Location")
                End If
            End Using
        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        results = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try
        redirecturl = redirecturl
        Return results
    End Function
    ''' <summary>
    ''' Http响应
    ''' </summary>
    ''' <param name="url">请求网址</param>
    ''' <param name="Headerdics">头文件固定KEY值字典类型泛型集合</param>
    ''' <param name="heard">头文件集合</param>
    ''' <param name="postdata">提交的字符串型数据</param>
    ''' <param name="cookieContainers">cookie容器</param>
    ''' <param name="redirecturl">头文件中的跳转链接</param>
    ''' <returns>返回响应字符串结果</returns>
    Public Function RequestPost(ByVal url As String, Headerdics As Dictionary(Of String, Object), ByVal heard As WebHeaderCollection, ByVal postdata As String, ByRef cookieContainers As CookieContainer, ByRef ResponseHeaders As WebHeaderCollection, ByRef redirecturl As String) As String
        If url = "" Then Return ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function

        'Dim domain = CStr(Regex.Match(url, "^(?:\w+://)?([^/?]*)").Groups(1).Value)

        'If domain.Contains("www.") = True Then
        '    domain = domain.Replace("www.", "")
        'Else
        '    domain = domain
        'End If
        Dim results As String = ""
        Try

            Dim myRequest = CType(WebRequest.Create(url), HttpWebRequest)
            Dim data = Encoding.UTF8.GetBytes(postdata)
            myRequest.Headers = heard
            myRequest.Method = "POST"
            For Each pair In Headerdics
                GetType(HttpWebRequest).GetProperty(pair.Key).SetValue(myRequest, pair.Value, Nothing)
            Next
            myRequest.CookieContainer = cookieContainers
            myRequest.ContentLength = data.Length
            Using stream = myRequest.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Using myResponse As HttpWebResponse = myRequest.GetResponse()
                If myResponse.ContentEncoding.ToLower().Contains("gzip") Then
                    Using stream = myResponse.GetResponseStream()
                        Using reader As New StreamReader(New System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress), Encoding.UTF8)
                            results = reader.ReadToEnd()
                        End Using
                    End Using
                ElseIf myResponse.ContentEncoding.ToLower().Contains("deflate") Then
                    Using stream = myResponse.GetResponseStream()
                        Using reader As New StreamReader(New System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Decompress), Encoding.UTF8)
                            results = reader.ReadToEnd()
                        End Using
                    End Using
                Else
                    Using stream As Stream = myResponse.GetResponseStream()
                        Using reader As New StreamReader(stream, Encoding.UTF8)
                            results = reader.ReadToEnd()
                        End Using
                    End Using
                End If
                If myResponse.Headers("Location") IsNot Nothing Then
                    redirecturl = myResponse.Headers("Location")
                End If
                ResponseHeaders = myResponse.Headers
            End Using
        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        results = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try

        Return results
    End Function

    Public Sub RequestDownloadFile(ByVal url As String, Headerdics As Dictionary(Of String, Object), ByVal heard As WebHeaderCollection, ByRef cookieContainers As CookieContainer, ByVal filePath As String)
        If url = "" Then Return
        Try
            Dim myRequest As HttpWebRequest = WebRequest.Create(url)
            myRequest.Headers = heard
            myRequest.Method = "GET"
            For Each pair In Headerdics
                GetType(HttpWebRequest).GetProperty(pair.Key).SetValue(myRequest, pair.Value, Nothing)
            Next
            myRequest.CookieContainer = cookieContainers

            Using response As HttpWebResponse = myRequest.GetResponse
                Using dataStream As Stream = response.GetResponseStream
                    If dataStream IsNot Nothing Then
                        Using fs As New FileStream(filePath, FileMode.Create)
                            dataStream.CopyTo(fs)
                        End Using
                        MsgBox("下载完成!")
                    End If
                End Using
            End Using

        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        Debug.Print(reader.ReadToEnd())
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try


    End Sub

    Public Async Function HttpClientPostAsync(ByVal url As String, Headerdics As Dictionary(Of String, Object), postdata As String, datatype As String, cookieContainers As CookieContainer, redirecturl As String) As Task(Of String)
        If url = "" Then Return ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function
        Dim res = ""
        Try
            Using handler = New HttpClientHandler()
                handler.CookieContainer = cookieContainers
                Using client = New HttpClient(handler)
                    For Each pair In Headerdics
                        client.DefaultRequestHeaders.TryAddWithoutValidation(pair.Key, pair.Value)
                    Next
                    Dim content As New Http.StringContent(postdata, System.Text.Encoding.UTF8, datatype)
                    Using HttpResponse As HttpResponseMessage = Await client.PostAsync(url, content)
                        HttpResponse.EnsureSuccessStatusCode()
                        If Not HttpResponse.Headers.Location Is Nothing Then
                            redirecturl = HttpResponse.Headers.Location.ToString
                        End If
                        If LCase(HttpResponse.ToString).Contains("gzip") Then
                            Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                Using gzipStream = New GZipStream(HttpResponseStream, CompressionMode.Decompress)
                                    Using streamReader = New StreamReader(gzipStream, Encoding.UTF8)
                                        res = streamReader.ReadToEnd()
                                    End Using
                                End Using
                            End Using
                        ElseIf LCase(HttpResponse.ToString).Contains("deflate") Then
                            Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                Using deflateStream = New DeflateStream(HttpResponseStream, CompressionMode.Decompress)
                                    Using streamReader = New StreamReader(deflateStream, Encoding.UTF8)
                                        res = streamReader.ReadToEnd()
                                    End Using
                                End Using
                            End Using
                        ElseIf LCase(HttpResponse.ToString).Contains("br") Then
                            Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                Using brStream = New BrotliStream(HttpResponseStream, CompressionMode.Decompress)
                                    Using streamReader = New StreamReader(brStream, Encoding.UTF8)
                                        res = streamReader.ReadToEnd()
                                    End Using
                                End Using
                            End Using
                        Else
                            Using HttpContent As HttpContent = HttpResponse.Content
                                res = Await HttpContent.ReadAsStringAsync()
                            End Using
                        End If
                    End Using
                End Using
            End Using
        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        res = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try

        Return res
    End Function
    Public Async Function HttpClientGetAsync(ByVal url As String, Headerdics As Dictionary(Of String, Object), cookieContainers As CookieContainer, redirecturl As String) As Task(Of String)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function
        Dim res = ""
        Try
            Using handler = New HttpClientHandler()
                handler.CookieContainer = cookieContainers
                Using client = New HttpClient(handler)
                    For Each pair In Headerdics
                        client.DefaultRequestHeaders.TryAddWithoutValidation(pair.Key, pair.Value)
                    Next
                    Using HttpResponse As HttpResponseMessage = Await client.GetAsync(url)
                        If HttpResponse.StatusCode = System.Net.HttpStatusCode.OK Then
                            If Not HttpResponse.Headers.Location Is Nothing Then
                                redirecturl = HttpResponse.Headers.Location.ToString
                            End If
                            If LCase(HttpResponse.ToString).Contains("gzip") Then
                                Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                    Using gzipStream = New GZipStream(HttpResponseStream, CompressionMode.Decompress)
                                        Using streamReader = New StreamReader(gzipStream, Encoding.UTF8)
                                            res = streamReader.ReadToEnd()
                                        End Using
                                    End Using
                                End Using
                            ElseIf LCase(HttpResponse.ToString).Contains("deflate") Then
                                Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                    Using deflateStream = New DeflateStream(HttpResponseStream, CompressionMode.Decompress)
                                        Using streamReader = New StreamReader(deflateStream, Encoding.UTF8)
                                            res = streamReader.ReadToEnd()
                                        End Using
                                    End Using
                                End Using
                            ElseIf LCase(HttpResponse.ToString).Contains("br") Then
                                Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                    Using brStream = New BrotliStream(HttpResponseStream, CompressionMode.Decompress)
                                        Using streamReader = New StreamReader(brStream, Encoding.UTF8)
                                            res = streamReader.ReadToEnd()
                                        End Using
                                    End Using
                                End Using
                            Else
                                Using HttpContent As HttpContent = HttpResponse.Content
                                    res = Await HttpContent.ReadAsStringAsync()
                                End Using
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        res = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try

        Return res
    End Function
    Public Async Function HttpClientPostFormAsync(ByVal url As String, Headerdics As Dictionary(Of String, Object), form As MultipartFormDataContent, cookieContainers As CookieContainer, redirecturl As String) As Task(Of String)
        If url = "" Then Return ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function
        Dim res = ""
        Try
            Using handler = New HttpClientHandler()
                handler.CookieContainer = cookieContainers
                Using client = New HttpClient(handler)
                    For Each pair In Headerdics
                        client.DefaultRequestHeaders.TryAddWithoutValidation(pair.Key, pair.Value)
                    Next
                    Using HttpResponse As HttpResponseMessage = Await client.PostAsync(url, form)
                        HttpResponse.EnsureSuccessStatusCode()
                        If Not HttpResponse.Headers.Location Is Nothing Then
                            redirecturl = HttpResponse.Headers.Location.ToString
                        End If
                        If LCase(HttpResponse.ToString).Contains("gzip") Then
                            Using HttpResponseStream As Stream = Await client.GetStreamAsync(url)
                                Using gzipStream = New GZipStream(HttpResponseStream, CompressionMode.Decompress)
                                    Using streamReader = New StreamReader(gzipStream, Encoding.UTF8)
                                        res = streamReader.ReadToEnd()
                                    End Using
                                End Using
                            End Using
                        Else
                            Using HttpContent As HttpContent = HttpResponse.Content
                                res = Await HttpContent.ReadAsStringAsync()
                            End Using
                        End If
                    End Using
                End Using
            End Using
        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        res = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try

        Return res
    End Function
    Public Async Function HttpClientDownloadFileAsync(ByVal url As String, Headerdics As Dictionary(Of String, Object), cookieContainers As CookieContainer， filepath As String) As Task(Of Boolean)
        If url = "" Then Return ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function
        Try
            Using client = New HttpClient(New HttpClientHandler() With {.CookieContainer = cookieContainers, .AutomaticDecompression = DecompressionMethods.None Or DecompressionMethods.Deflate Or DecompressionMethods.GZip})
                For Each pair In Headerdics
                    client.DefaultRequestHeaders.TryAddWithoutValidation(pair.Key, pair.Value)
                Next
                Using HttpResponse As HttpResponseMessage = Await client.GetAsync(url)
                    If HttpResponse.StatusCode = System.Net.HttpStatusCode.OK Then
                        Dim buffer() As Byte = Nothing
                        Using task As Stream = Await HttpResponse.Content.ReadAsStreamAsync()
                            Using ms As New MemoryStream()
                                Await task.CopyToAsync(ms)
                                buffer = ms.ToArray()
                            End Using
                            File.WriteAllBytes(filepath, buffer)
                            MsgBox("下载完成!")
                            buffer = Nothing
                            Return True
                        End Using
                    End If
                End Using
            End Using

        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        Return False
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try

        Return False
    End Function

    Public Async Function HttpClientGetRedirectLink(ByVal url As String, Headerdics As Dictionary(Of String, Object), cookieContainers As CookieContainer) As Task(Of String)
        If url = "" Then Return ""
        Dim res = ""
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11
        ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                      Return True
                                                                  End Function
        Try
            Using client = New HttpClient(New HttpClientHandler() With {.CookieContainer = cookieContainers, .AutomaticDecompression = DecompressionMethods.None Or DecompressionMethods.Deflate Or DecompressionMethods.GZip}) With {.Timeout = TimeSpan.FromSeconds(30)}
                For Each pair In Headerdics
                    client.DefaultRequestHeaders.TryAddWithoutValidation(pair.Key, pair.Value)
                Next
                Dim response As HttpResponseMessage = Await client.GetAsync(url)
                Dim statusCode = Math.Truncate(response.StatusCode)
                If statusCode >= 300 AndAlso statusCode <= 399 Then
                    Return response.Headers.Location.ToString
                ElseIf Not response.IsSuccessStatusCode Then
                    Dim responseUri As String = response.RequestMessage.RequestUri.ToString()
                    Console.Out.WriteLine(responseUri)
                Else
                    Return response.RequestMessage.RequestUri.ToString()
                End If
            End Using
        Catch e As WebException
            Using response As WebResponse = e.Response
                Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                Console.WriteLine("Error code: {0}", httpResponse.StatusCode)
                Using data As Stream = response.GetResponseStream()
                    Using reader = New StreamReader(data)
                        res = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                Debug.Print(ex.GetBaseException.Message.ToString)
            Else
                Debug.Print(ex.Message.ToString)
            End If
        End Try

        Return res
    End Function
End Module
