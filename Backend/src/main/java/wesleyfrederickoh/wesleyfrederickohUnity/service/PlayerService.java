package wesleyfrederickoh.wesleyfrederickohUnity.service;

import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerPublicDTO;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerUpgradeDTO;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.model.PlayerSkill;
import wesleyfrederickoh.wesleyfrederickohUnity.model.PlayerUpgrade;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Skill;
import wesleyfrederickoh.wesleyfrederickohUnity.model.UpgradeType;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerSkillRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerUpgradeRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.SkillRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.UpgradeTypeRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
public class PlayerService {
    @Autowired
    private PlayerRepository playerRepository;

    @Autowired
    private UpgradeTypeRepository upgradeTypeRepository;

    @Autowired
    private PlayerUpgradeRepository playerUpgradeRepository;

    @Autowired
    private SkillRepository skillRepository;

    @Autowired
    private PlayerSkillRepository playerSkillRepository;

    @Transactional
    public Player createPlayer(Player player) {
        player.setCurrency(0);
        Player savedPlayer = playerRepository.save(player);

        initializeDefaultUpgrades(savedPlayer);
        initializeDefaultSkills(savedPlayer);

        return savedPlayer;
    }

    private void initializeDefaultUpgrades(Player player) {
        String[] defaultUpgradeNames = {"Production", "Click"};
        for (String upgradeName : defaultUpgradeNames) {
            UpgradeType upgradeType = upgradeTypeRepository.findByName(upgradeName)
                    .orElseThrow(() -> new RuntimeException("Default upgrade type not found: " + upgradeName));
            PlayerUpgrade playerUpgrade = new PlayerUpgrade(player, upgradeType, 0);
            playerUpgradeRepository.save(playerUpgrade);
        }
    }

    private void initializeDefaultSkills(Player player) {
        String[] defaultSkillNames = {"DoubleProduction", "DoubleClick"};
        for (String skillName : defaultSkillNames) {
            Skill skill = skillRepository.findByName(skillName)
                    .orElseThrow(() -> new RuntimeException("Default skill not found: " + skillName));
            PlayerSkill playerSkill = new PlayerSkill(player, skill);
            playerSkillRepository.save(playerSkill);
        }
    }

    public Optional<Player> getPlayerByUsername(String username) {
        return playerRepository.findByUsername(username);
    }

    public Optional<Player> getPlayerByEmailAndPassword(String email, String password) {
        return playerRepository.findByEmailAndPassword(email, password);
    }

    public Optional<Long> getCurrencyByUsername(String username) {
        return playerRepository.findByUsername(username).map(Player::getCurrency);
    }

    public List<PlayerPublicDTO> getRandomPlayers(UUID excludePlayerId) {
        List<Player> randomPlayers = playerRepository.findRandomPlayers(excludePlayerId);

        return randomPlayers.stream()
                .map(player -> new PlayerPublicDTO(
                        player.getId(),
                        player.getUsername(),
                        player.getCurrency()))
                .collect(Collectors.toList());
    }

    public List<PlayerUpgradeDTO> getUpgradesForPlayer(String username) {
        List<Map<String, Object>> results = playerRepository.findAllUpgradesByUsername(username);
        return results.stream()
                .map(result -> new PlayerUpgradeDTO(
                        (String) result.get("name"),
                        (String) result.get("description"),
                        ((Number) result.get("basecost")).longValue(),
                        ((Number) result.get("costmult")).doubleValue(),
                        (Integer) result.get("level")
                ))
                .collect(Collectors.toList());
    }
}
