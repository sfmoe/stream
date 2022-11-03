window.addEventListener("load",async ()=>{
const queryString = window.location.search;
let URLParams = new URLSearchParams(queryString);
let password = URLParams.get("password");
const obs = new OBSWebSocket();
let source = URLParams.get("source") || "Mic/Aux";
let port = URLParams.get("port") || "4455";
if(!password){
    document.querySelector(".error").classList.add("show-error"); 
}
    try{
        await obs.connect(`ws:127.0.0.01:${port}`, password);
        const { inputMuted } = await obs.call("GetInputMute", {
            inputName: source,
          });
          
          document.querySelector(".mute-status").classList.toggle("muted", inputMuted);

          obs.on("InputMuteStateChanged", (data) => {
            if(data.inputName === source){
                document.querySelector(".mute-status").classList.toggle("muted", data.inputMuted);
            }
          });

    } catch (error) {
        //hide the icon if its showing for some reason and something goes wrong connecting
        document.querySelector(".mute-status").classList.remove("muted");
    }
});
