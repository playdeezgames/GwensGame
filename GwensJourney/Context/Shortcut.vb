Friend Class Shortcut
    Implements IShortcut

    Private _context As Context

    Public Sub New(context As Context)
        Me._context = context
    End Sub

    Public ReadOnly Property HasShortcut As Boolean Implements IShortcut.HasShortcut
        Get
            Return _context.Counter(CounterNames.ShortcutDistance) <> 0
        End Get
    End Property

    Public ReadOnly Property Distance As Integer Implements IShortcut.Distance
        Get
            Return _context.Counter(CounterNames.ShortcutDistance)
        End Get
    End Property

    Public Sub Generate() Implements IShortcut.Generate
        Select Case RNG.FromRange(0, 1)
            Case 0
                _context.Counter(CounterNames.ShortcutDistance) = 0
            Case Else
                _context.Counter(CounterNames.ShortcutDistance) = RNG.FromRange(-10, 10)
        End Select
    End Sub
End Class
