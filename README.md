# Spikes.Tardigrade

Test Tardigrade with .Net

It uses https://github.com/alex75it/uplink.net

![Build](https://github.com/alex75it/Spikes.Tardigrade/workflows/Build/badge.svg)

(Dec 2020) There is no official .Net support and the documentation for the non-official library is wrong or old.  
It is also a simple wrapper of the C DLL and the call has to called with key name and secret inverted otherwise you have this error:
> "uplink: api key format error: invalid api key format"

I spent half an hour on the website to find the Satellite Address, NodeId and Port to use for the Satellite without finding it.  
  
Finally tried to use this address and it works: **europe-west-1.tardigrade.io:7777**


## Functionalities:
- WriteFile
- ReadFile



## TODO:
  - DeleteFile
  - ListFiles