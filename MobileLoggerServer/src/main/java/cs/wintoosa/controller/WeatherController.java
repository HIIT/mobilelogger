/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Text;
import cs.wintoosa.domain.Weather;
import cs.wintoosa.service.ILogService;
import java.util.List;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author vkukkola
 */
@Controller
@RequestMapping(value = "/log/weather")
public class WeatherController {
    @Autowired
    ILogService logService;
    
    @RequestMapping(method=RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putLog(@Valid @RequestBody Weather data) {
        //System.out.println("at Weather controller, log is: "+data);
        return logService.saveLog(data);
    }
    
    @RequestMapping(method=RequestMethod.GET)
    @ResponseBody
    public List<Abstractlog> getLogs(){
        return logService.getAll(Weather.class);
    }
}
