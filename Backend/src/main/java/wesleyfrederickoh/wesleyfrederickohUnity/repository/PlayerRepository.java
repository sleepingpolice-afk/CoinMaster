package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.repository.query.Param;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import org.springframework.data.jpa.repository.Query;

import java.util.Optional;
import java.util.UUID;
import java.util.List;

@Repository
public interface PlayerRepository extends JpaRepository<Player, UUID> {
    Optional<Player> findByUsername(String username);
    Optional<Player> findByEmail(String email);
    boolean existsByUsername(String username);
    boolean existsByEmail(String email);
    Optional<Player> findByEmailAndPassword(String email, String password);

    @Query(value = "SELECT * FROM player WHERE playerid != :excludePlayerId ORDER BY RANDOM() LIMIT 5", nativeQuery = true)
    List<Player> findRandomPlayers(@Param("excludePlayerId") UUID excludePlayerId);
}
