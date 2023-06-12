Module Program
    Sub Main(args As String())
        Dim engine As New Engine()
        Dim frameBuffer As IFrameBuffer = New FrameBuffer(80, 25)
        Using host As New Host(FrameBuffer, engine, engine, 1, False)
            host.Run()
        End Using
    End Sub
End Module
