# Spikes.Tardigrade

Test Tardigrade with .Net

https://github.com/alex75it/uplink.net


2020-11 Give up because there is no official .Net support and the documentation for the non-official library is wrong or old.  
It is also a simple wrapper of the C DLL and the simple call fail with the provided key name nd secret with this message:
> "uplink: api key format error: invalid api key format"

Also, I spent half an hour on the website to find wha Address, NodeId and Port to use for the Satellite without finding it.  
  
I inverted the use of the API name and key and now I have this error:  
> "uplink: node URL error: unknown scheme "https" "

This is the used URL: https://europe-west-1.tardigrade.io:7777

The scheme HTTPS is not required, just "europe-west-1.tardigrade.io:7777" is ok.