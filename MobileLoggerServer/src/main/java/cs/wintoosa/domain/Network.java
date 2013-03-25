/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.domain;

import javax.persistence.Entity;

/**
 *
 * @author vkukkola, jonimake
 */
@Entity
public class Network extends Log{

    private String operator;
    private String InterfaceBandwidth;
    private String InterfaceCharacteristics;
    private String InterfaceDescription;
    private String InterfaceName;
    private String InterfaceState;
    private String InterfaceSubtype;
    private String InterfaceType;

    public String getOperator() {
        return operator;
    }

    public void setOperator(String operator) {
        this.operator = operator;
    }

    public String getInterfaceBandwidth() {
        return InterfaceBandwidth;
    }

    public void setInterfaceBandwidth(String InterfaceBandwidth) {
        this.InterfaceBandwidth = InterfaceBandwidth;
    }

    public String getInterfaceCharacteristics() {
        return InterfaceCharacteristics;
    }

    public void setInterfaceCharacteristics(String InterfaceCharacteristics) {
        this.InterfaceCharacteristics = InterfaceCharacteristics;
    }

    public String getInterfaceDescription() {
        return InterfaceDescription;
    }

    public void setInterfaceDescription(String InterfaceDescription) {
        this.InterfaceDescription = InterfaceDescription;
    }

    public String getInterfaceName() {
        return InterfaceName;
    }

    public void setInterfaceName(String InterfaceName) {
        this.InterfaceName = InterfaceName;
    }

    public String getInterfaceState() {
        return InterfaceState;
    }

    public void setInterfaceState(String InterfaceState) {
        this.InterfaceState = InterfaceState;
    }

    public String getInterfaceSubtype() {
        return InterfaceSubtype;
    }

    public void setInterfaceSubtype(String InterfaceSubtype) {
        this.InterfaceSubtype = InterfaceSubtype;
    }

    public String getInterfaceType() {
        return InterfaceType;
    }

    public void setInterfaceType(String InterfaceType) {
        this.InterfaceType = InterfaceType;
    }
    
    
    
}
