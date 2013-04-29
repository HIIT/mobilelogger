package cs.wintoosa.domain;

import javax.persistence.Entity;

/**
 *
 * @author jonimake
 */
@Entity
public class Touch extends Abstractlog {

    //x coordinate on the phone screen
    private int xcoord;
    
    //y coordinate on the phone screen
    private int ycoord;
    
    //what sort of action (down, up, move)
    private String action;

    public void setXcoord(int xcoord) {
        this.xcoord = xcoord;
    }

    public int getXcoord() {
        return xcoord;
    }
    

    public void setYcoord(int ycoord) {
        this.ycoord = ycoord;
    }

    public int getYcoord() {
        return ycoord;
    }
   

    public void setAction(String action) {
        this.action = action;
    }

    public String getAction() {
        return action;
    }
}
