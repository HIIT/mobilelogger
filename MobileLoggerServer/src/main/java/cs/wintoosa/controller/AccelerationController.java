/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.controller.interceptor.ValidationInterceptor;
import cs.wintoosa.domain.AccLog;
import cs.wintoosa.domain.Log;
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
@RequestMapping(value = "/log/accel")
public class AccelerationController {
    @Autowired
    ILogService logService;
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public List<Log> getLogs() {
        return logService.getAll(AccLog.class);
    }
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putAccelerationLog( @RequestBody AccLog log, BindingResult result) {
        System.out.println("put accel");
        return logService.saveLog(log);
    }
}
