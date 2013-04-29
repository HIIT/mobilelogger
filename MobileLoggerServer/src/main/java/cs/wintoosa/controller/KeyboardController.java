/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.KeyPress;
import cs.wintoosa.domain.Keyboard;
import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.service.ILogService;
import java.util.List;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author jonimake
 */
@RequestMapping("/log")
@Controller
public class KeyboardController {
    
    @Autowired
    ILogService logService;
    
    @RequestMapping(value= "/keyboard", method= RequestMethod.PUT,  consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean put(@Valid @RequestBody Keyboard keyboard) {
        return logService.saveLog(keyboard);
    }
    
    @RequestMapping(value= "/keyPress", method= RequestMethod.PUT,  consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putPress(@Valid @RequestBody KeyPress keypress) {
        return logService.saveLog(keypress);
    }
    
    
    @RequestMapping(value = "/keyPress", method= RequestMethod.GET)
    @ResponseBody
    public List<Abstractlog> getKeypressLogs() {
        return logService.getAll(KeyPress.class);
    }
    
    @RequestMapping(value = "/keyboard", method= RequestMethod.GET)
    @ResponseBody
    public List<Abstractlog> getLogs() {
        return logService.getAll(Keyboard.class);
    }
}
