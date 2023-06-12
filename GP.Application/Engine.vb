Imports System.ComponentModel

Public Class Engine
    Inherits BasePresenter
    Implements IEngine
    Private ReadOnly _config As GPConfig
    Private ReadOnly _frameBuffer As IFrameBuffer
    Private _lineBuffer As String
    Sub New(config As GPConfig, frameBuffer As IFrameBuffer)
        _config = config
        _frameBuffer = frameBuffer
    End Sub
    Public Sub Update(commands As IEnumerable(Of String), ticks As Long) Implements IEngine.Update
        For Each command In commands
            If command.Length = 1 Then
                Select Case command
                    Case ChrW(8)
                        _frameBuffer.CursorColumn -= 1
                        _frameBuffer.Write(" "c)
                        _frameBuffer.CursorColumn -= 1
                        _lineBuffer = If(_lineBuffer.Any, _lineBuffer.Substring(0, _lineBuffer.Length - 1), _lineBuffer)
                    Case ChrW(13)
                        _frameBuffer.WriteLine("")
                        Execute()
                        _lineBuffer = String.Empty
                    Case Else
                        _frameBuffer.Write(command)
                        _lineBuffer &= command
                End Select
            End If
        Next
    End Sub

    Public Sub Initialize() Implements IEngine.Initialize
        _frameBuffer.Clear()
        _frameBuffer.ForegroundColor = LightRed
        _frameBuffer.WriteLine("                          ___                 _")
        _frameBuffer.WriteLine("                         / __|_ __ _____ _ _ ( )___")
        _frameBuffer.WriteLine("                        | (_ \ V  V / -_) ' \|/(_-<")
        _frameBuffer.WriteLine("              ___        \___|\_/\_/\___|_||_| /__/_   _")
        _frameBuffer.WriteLine("             | _ \___ _ _ ___ __ _ _ _(_)_ _  __ _| |_(_)___ _ _")
        _frameBuffer.WriteLine("             |  _/ -_) '_/ -_) _` | '_| | ' \/ _` |  _| / _ \ ' \")
        _frameBuffer.WriteLine("             |_| \___|_| \___\__, |_| |_|_||_\__,_|\__|_\___/_||_|")
        _frameBuffer.WriteLine("                             |___/")
        _frameBuffer.ForegroundColor = White
        _frameBuffer.WriteLine("                              Gwen's Peregrination")
        _frameBuffer.ForegroundColor = Gray
        _frameBuffer.WriteLine("                        A Production of TheGrumpyGameDev")
    End Sub

    Private Sub Execute()
        Throw New NotImplementedException()
    End Sub
End Class
