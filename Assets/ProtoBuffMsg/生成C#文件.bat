cd..
cd..
set f=%1
set out=%f:~0,-5%cs
"protobuf-net r668\ProtoGen\protogen.exe" -i:%1 -o:%out%