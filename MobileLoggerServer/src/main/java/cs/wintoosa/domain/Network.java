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
    private String interfaceBandwidth;
    private String interfaceCharacteristics;
    private String interfaceDescription;
    private String interfaceName;
    private String interfaceState;
    private String interfaceSubtype;
    private String interfaceType;

    public String getOperator() {
        return operator;
    }

    public void setOperator(String operator) {
        this.operator = operator;
    }

    public String getInterfaceBandwidth() {
        return interfaceBandwidth;
    }

    public void setInterfaceBandwidth(String InterfaceBandwidth) {
        this.interfaceBandwidth = InterfaceBandwidth;
    }

    public String getInterfaceCharacteristics() {
        return interfaceCharacteristics;
    }

    public void setInterfaceCharacteristics(String InterfaceCharacteristics) {
        this.interfaceCharacteristics = InterfaceCharacteristics;
    }

    public String getInterfaceDescription() {
        return interfaceDescription;
    }

    public void setInterfaceDescription(String InterfaceDescription) {
        this.interfaceDescription = InterfaceDescription;
    }

    public String getInterfaceName() {
        return interfaceName;
    }

    public void setInterfaceName(String InterfaceName) {
        this.interfaceName = InterfaceName;
    }

    public String getInterfaceState() {
        return interfaceState;
    }

    public void setInterfaceState(String InterfaceState) {
        this.interfaceState = InterfaceState;
    }

    public String getInterfaceSubtype() {
        return interfaceSubtype;
    }

    public void setInterfaceSubtype(String InterfaceSubtype) {
        this.interfaceSubtype = InterfaceSubtype;
    }

    public String getInterfaceType() {
        return interfaceType;
    }

    public void setInterfaceType(String InterfaceType) {
        this.interfaceType = InterfaceType;
    }
 
    
    
}
