package cs.wintoosa.service;

import java.util.List;

/**
 *
 * @author jonimake
 */
public interface IGenericLogService<T> {
    public List<T> getAll();
}
