# OBS Muted overlay

A quick little overlay to show when a source is muted.

## Installation
---

**REQUIRES** OBS websocket to be turned on

Create a browser source where you want the overlay and enter the following url pointing to where you are hosting this page.

```sh
http://<url.to.host>/obs_muted/index.html?password=<WEBSOCKET_PASSWORD>
```

By default this connects to the new websocket offered in OBS version 28+ on localhost port **4455** 


### Optional URL Variable options
---
The following options can be changed via URL query parameters. 

**source** : default is "Mix/Aux"

**port** : default is 4455