Module Program
    Sub Main(args As String())
        Dim config = GPConfig.Load()
        Dim engine As New Engine(config)
        Dim frameBuffer As IFrameBuffer = New FrameBuffer(80, 25)
        frameBuffer.WriteLine("Welcome to Gwen's Peregrination!")
        Using host As New Host(
            "Gwen's Peregrination",
            frameBuffer,
            engine,
            engine,
            config.Scale,
            config.FullScreen,
            config.Volume,
            New Dictionary(Of String, String))
            host.Run()
        End Using
    End Sub
End Module
