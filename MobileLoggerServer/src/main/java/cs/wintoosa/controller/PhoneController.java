/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.controller;

import cs.wintoosa.domain.SessionLog;
import cs.wintoosa.service.ILogService;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 *
 * @author jonimake
 */
@Controller
@RequestMapping(value="/log/phone")
public class PhoneController {
    
    @Autowired
    ILogService logService;
    
    @RequestMapping(method= RequestMethod.GET)
    public String listAll(Model model) {
        
        model.addAttribute("phones", logService.getAllPhones());
        
        return "phone";
    }
    
    @RequestMapping(value="/{phoneId}", method= RequestMethod.GET)
    public String getByPhoneId(@PathVariable String phoneId, Model model) {
        System.out.println(logService.getSessionByPhoneId(phoneId).size());
        model.addAttribute("sessions", logService.getSessionByPhoneId(phoneId));
        return "session";
    }
}
