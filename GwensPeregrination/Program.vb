Module Program
    Sub Main(args As String())
        Dim config = GPConfig.Load()
        Dim engine As New Engine(config)
        Dim frameBuffer As IFrameBuffer = New FrameBuffer(80, 25, Gray, Black)
        frameBuffer.Clear()
        frameBuffer.ForegroundColor = LightRed
        frameBuffer.WriteLine("                          ___                 _")
        frameBuffer.WriteLine("                         / __|_ __ _____ _ _ ( )___")
        frameBuffer.WriteLine("                        | (_ \ V  V / -_) ' \|/(_-<")
        frameBuffer.WriteLine("              ___        \___|\_/\_/\___|_||_| /__/_   _")
        frameBuffer.WriteLine("             | _ \___ _ _ ___ __ _ _ _(_)_ _  __ _| |_(_)___ _ _")
        frameBuffer.WriteLine("             |  _/ -_) '_/ -_) _` | '_| | ' \/ _` |  _| / _ \ ' \")
        frameBuffer.WriteLine("             |_| \___|_| \___\__, |_| |_|_||_\__,_|\__|_\___/_||_|")
        frameBuffer.WriteLine("                             |___/")
        frameBuffer.ForegroundColor = White
        frameBuffer.WriteLine("                              Gwen's Peregrination")
        frameBuffer.ForegroundColor = Gray
        frameBuffer.WriteLine("                        A Production of TheGrumpyGameDev")
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
