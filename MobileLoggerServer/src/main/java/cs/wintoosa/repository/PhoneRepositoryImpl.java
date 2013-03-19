/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package cs.wintoosa.repository;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

/**
 *
 * @author jonimake
 */
public class PhoneRepositoryImpl implements IPhoneRepositoryCustom{
    @PersistenceContext
    EntityManager em;
}
