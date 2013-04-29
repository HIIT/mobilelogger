/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.Abstractlog;
import cs.wintoosa.domain.Network;
import cs.wintoosa.service.ILogService;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author vkukkola
 */
@Controller
@RequestMapping(value = "/log/network")
public class NetworkController {
    @Autowired
    ILogService logService;
    
    @RequestMapping(method= RequestMethod.GET)
    @ResponseBody
    public List<Abstractlog> getLogs() {
        return logService.getAll(Network.class);
    }
    
    @RequestMapping(method= RequestMethod.PUT, consumes=MediaType.APPLICATION_JSON_VALUE)
    @ResponseBody
    public boolean putNetworkLog(@RequestBody Network log) {
        return logService.saveLog(log);
    }
}
