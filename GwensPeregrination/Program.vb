Module Program
    Sub Main(args As String())
        Dim config = GPConfig.Load()
        Dim frameBuffer As IFrameBuffer = New FrameBuffer(80, 25, Gray, Black)
        Dim engine As New Engine(config, frameBuffer)
        Using host As New Host(
            "Gwen's Peregrination",
            frameBuffer,
            engine,
            engine,
            config.Scale,
            config.FullScreen,
            config.Volume,
            New Dictionary(Of String, String) From
            {
                {PlayerHit, "Content/PlayerHit.wav"},
                {PlayerDead, "Content/PlayerDead.wav"}
            })
            AddHandler Engine.OnPlaySfx, AddressOf host.HandleSfx
            host.Run()
        End Using
    End Sub
End Module
