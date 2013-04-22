/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import cs.wintoosa.domain.Log;
import cs.wintoosa.domain.TextLog;
import cs.wintoosa.service.ILogService;
import java.util.List;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.validation.ObjectError;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author vkukkola
 */
@Controller
@RequestMapping(value = "/log/clicked")
public class SearchClickedController {
    @Autowired
    ILogService logService;
    
    @RequestMapping(method=RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putLog(@Valid @RequestBody TextLog data, BindingResult result) {
        //System.out.println("at result Clicked controller, log is: "+data);
        data.setType("clicked");
        return logService.saveLog(data);
    }
    
    @RequestMapping(method=RequestMethod.GET)
    @ResponseBody
    public List<TextLog> getLogs(){
        return logService.getTextLogByType("clicked");
    }
}
