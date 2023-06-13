dotnet publish ./GwensPeregrination/GwensPeregrination.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./GwensPeregrination/GwensPeregrination.vbproj -o ./pub-windows -c Release --sc -r win-x64
dotnet publish ./GwensPeregrination/GwensPeregrination.vbproj -o ./pub-mac -c Release --sc -r osx-x64
butler push pub-windows thegrumpygamedev/gwens-peregrination:windows
butler push pub-linux thegrumpygamedev/gwens-peregrination:linux
butler push pub-mac thegrumpygamedev/gwens-peregrination:mac
git add -A
git commit -m "shipped it!"