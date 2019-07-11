formImportData.push({type:"label",list:[{type:"button",value:"保存",command:"save"},{type:"newcolumn"},{type:"button",value:"取消",command:"close"}]});
var w=this.create(id,350,120);
w.setText("<span style='font-size:14px'>"+title+"</span>");
var fom=new Array();
var formId="formId_"+new Date().getTime();
fom.push('<form id="');
fom.push(formId);
fom.push('" style="overflow: hidden;" target="if');
fom.push(formId);
fom.push('" accept-charset="UTF-8" method="POST" action="');
fom.push(url);
fom.push('" enctype="multipart/form-data">');
fom.push('</form><iframe id="if');
fom.push(formId);
fom.push('" name="if')
fom.push(formId);
fom.push('" width="0" height="0" frameborder="0" onload="FileUplaodEvent(this);"></iframe>');
w.attachHTMLString(fom.join(""));
$("#if"+formId).data("load",function(obj){
    if(obj.success){
        w.close();
    }else{}
    if(fun){
        fun(obj);
    }
    alert(obj.message);

});
var w1form=new dhtmlXForm(formId,formImportData);
w1form.attachEvent("onButtonClick",function(name,command){
    switch(command){
        case "save":
            $("#"+formId).submit();
            break;
        case "close":
            w.close();
            break;
    }
});
return w;
var w=this.create(id,350,120);

var w = dhtmxWin.create("updatepassowrd",100,100);