dhtmlXGridObject.prototype.getCheckValue=function(){
	var v=new Array();
	for(i=0;i<this.getColumnCount();i++){
		if(this.getColType(i)=="ch"){
			this.forEachRow(function(id){
				if(this.cells(id,i).getValue()==1){
					v.push(id);
				}
			});
			break;
		}
	}
	return v;
}
dhtmlXToolbarObject.prototype.addGridState=function(id,index,grid){
	this.addButtonTwoState(id,index,"全选","iconUncheckAll.gif");
	this.attachEvent("onStateChange",function(id,state){
		if(state){
			this.setItemImage(id,"iconCheckAll.gif");
		}else{
			this.setItemImage(id,"iconUncheckAll.gif");
		}
		if(grid.xmlFileUrl){
			grid.xmlFileUrl=grid.xmlFileUrl+(grid.xmlFileUrl.indexOf("?")==-1?"?":"&")+"checkState="+state;
		}
		grid.checkState=state;
		grid.checkAll(state);
	});
}
dhtmlXForm.prototype.getFormValues=function(a,b){
	var c=this.getFormData(a,b);
	$(this.base).find("input[type='text']").each(function(i,n){
		if(c[n.name]==null||typeof c[n.name]=="string"){
			c[n.name]=n.value;
		}
	});
	return c;
}

dhtmlXGridObject.prototype.send = function (url, form, param, fun) {
    var e = [], f;
    var self = this;
    if (form) {
        if (form.validate()) {
            var d = form.getFormValues();
            for (f in d) {
                if (d[f]) {
                    if (d[f].isData) {
                        e.push(f + "=" + d[f].format('yyyy-MM-dd hh:mm:ss'));
                    } else {
                        e.push(f + "=" + encodeURIComponent(d[f]));
                    }
                }
            }
        } else {
            return false;
        }
    }
    for (f in param) {
        if (param[f].isData) {
            e.push(f + "=" + param[f].format('yyyy-MM-dd hh:mm:ss'));
        } else {
            e.push(f + "=" + encodeURIComponent(param[f]));
        }
    }
    if (this.checkState) {
        e.push("checkState=" + self.checkState);
    }
    url = url + (url.indexOf("?") == -1 ? "?" : "&") + e.join("&");
    self.clearAndLoad(url, "json");
}
dhtmlXWindows.prototype.create=function(id,width,height){
	var w=(document.body.clientWidth-width)/2;
	if(w<0)
		w=0;
	var h=(document.body.clientHeight-height)/2;
	if(h<0)
		h=0;
	return this.createWindow(id,w,h,width,height);
}
dhtmlXWindows.prototype.createUpload=function(id,title,url,formImportData,fun){
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
}
dhtmlXWindows.prototype.createPlay=function(id,url){
	var w=this.create(id,400,90);
	w.setText("<span style='font-size:14px'>播放</span>");
	var fom=new Array();
	var formId="play_"+new Date().getTime();
	fom.push('<EMBED id=')
	fom.push(formId);
	fom.push(' src="')
	fom.push(url);
	fom.push('" width="360" height="30" autoStart="true" type="audio/wav" controls="console" volume="50" loop="false">');
	w.attachHTMLString(fom.join(""));
	w.attachEvent("onClose", function() {
		try{$("#"+formId)[0].stop();}catch(e){}
		return true;
    });
	return w;
}
PlayAudio = function (data, type) {
    if (1 === type) {
        dhxWins.createPlay("play", "http:" + data.TS_FILE_URL.replace(/\\/ig, "/"));
    } else {
        open("http:" + data.TS_FILE_URL.replace(/\\/ig, "/"));
    }
}
var FileUplaodEvent=function(obj){
	var v=obj.contentDocument?obj.contentDocument.body.innerHTML:obj.document.body.innerHTML;
	if(""!=v){
		try{
			v=v.replace(/<.+?>/g,"");
			var o=$.parseJSON(v);
			$(obj).data("load")(o);
		}catch(e){
			alert(v);
		}
	}
}
dhtmlXCalendarObject.prototype.langData["cn"] = {
	    monthesFNames : ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
	    monthesSNames : ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
	    daysFNames : ["日", "一	", "二", "三", "四", "五", "六"],
	    daysSNames : ["日", "一	", "二", "三", "四", "五", "六"],
	    weekstart : 7
	}
Number.prototype.toData=function(){
	var d="";
	var v=this;
	for( var i=0;i<3;i++){
		var s=v%60;
		v=Math.floor(v/60);
		d=(s>9?":"+s:":0"+s)+d;
	}
	return d.substr(1);
}
Date.prototype.isData=true;
Date.prototype.format=function(fmt){
	var o={"M+":this.getMonth()+1,"d+":this.getDate(),"h+":this.getHours()%12==0?12:this.getHours()%12,"H+":this.getHours(),"m+":this.getMinutes(),"s+":this.getSeconds(),
		"q+":Math.floor((this.getMonth()+3)/3),"S":this.getMilliseconds()};
	var week={"0":"\u65e5","1":"\u4e00","2":"\u4e8c","3":"\u4e09","4":"\u56db","5":"\u4e94","6":"\u516d"};
	if(/(y+)/.test(fmt)){
		fmt=fmt.replace(RegExp.$1,(this.getFullYear()+"").substr(4-RegExp.$1.length));
	}
	if(/(E+)/.test(fmt)){
		fmt=fmt.replace(RegExp.$1,((RegExp.$1.length>1)?(RegExp.$1.length>2?"\u661f\u671f":"\u5468"):"")+week[this.getDay()+""]);
	}
	for( var k in o){
		if(new RegExp("("+k+")").test(fmt)){
			fmt=fmt.replace(RegExp.$1,(RegExp.$1.length==1)?(o[k]):(("00"+o[k]).substr((""+o[k]).length)));
		}
	}
	return fmt;
}
String.prototype.format=function(){
	var args=arguments;
	return this.replace(/\{(\d+)\}/g,function(m,i){
		return args[i];
	});
}
String.prototype.trim=function(){
	return this.replace(/(^\s*)|(\s*$)/g,"");
}
$.async=function(url,param){
    return $.parseJSON($.ajax({ type: "POST", url: url, async: false, data: param }).responseText);
}
$.host = location.toString().replace(/^(http:[\/]{2}[^\/]*)\/.*$/g, function(m, n) {
    //return n;
	return "http://192.168.8.44:8080";
});
var ActionPage=function(){
	var self=this;
	this.add=function(url,fun,data,param,after){
		if(self[fun]){
			self[fun](data,param,after);
		}else{
			$.getScript(url,function(){
				self[fun](data,param,after);
			});
		}
	}
}
ActionPage= new ActionPage();
dhtmlXMenuObject.prototype.loadJson = function (url, param) {
    var self = this;
    var l = 1;
    var addNode = function (array) {
        for (var i = 0; array && i < array.length; i++) {
            self.addNewChild(array[i].pid, l++, array[i].id, array[i].text, array[i].disabled);
            self.setUserData(array[i].id, "data", array[i].userdata);
            addNode(array[i].child);
        }
    };
    $.get(url, param, function (data) {
        var tem = { id: null, pid: null };
        for (var i = 0; i < data.Root.length; i++) {
            self.addNewSibling(tem.id, data.Root[i].id, data.Root[i].text, data.Root[i].disabled);
            addNode(data.Root[i].child);
            tem = data.Root[i];
        }
    }, "json");
};